using Microsoft.Identity.Client;
using System;
using System.Runtime.Caching;

namespace FileshareToOneDrive
{
        /// <summary>
        /// An implementation of token cache for Confidential Clients
        /// </summary>
        public class MSALCache
        {
            private readonly string appId;
            private readonly string cacheId;

            private readonly MemoryCache memoryCache = MemoryCache.Default;
            private readonly DateTimeOffset cacheDuration = DateTimeOffset.Now.AddHours(12);
            private readonly TokenCache cache = new TokenCache();

            public MSALCache(string clientId)
            {
                // not object, we want the subject
                this.appId = clientId;
                this.cacheId = this.appId + "_TokenCache";
                this.Load();
            }

            public void Load()
            {
                // Ideally, methods that load and persist should be thread safe.
                byte[] tokenCacheBytes = (byte[])this.memoryCache.Get(this.cacheId);
                if (tokenCacheBytes != null)
                {
                    this.cache.Deserialize(tokenCacheBytes);
                }
            }

            public void Persist()
            {
                // Ideally, methods that load and persist should be thread safe.

                // Optimistically set HasStateChanged to false. We need to do it early to avoid losing changes made by a concurrent thread.
                this.cache.HasStateChanged = false;

                // Reflect changes in the persistent store
                this.memoryCache.Set(this.cacheId, this.cache.Serialize(), this.cacheDuration);
            }

            public void Clear()
            {
                // Optimistically set HasStateChanged to false. We need to do it early to avoid losing changes made by a concurrent thread.
                this.cache.HasStateChanged = false;

                // Reflect changes in the persistent store
                this.memoryCache.Remove(this.cacheId);

                this.Load(); // Nulls the currently deserialized instance
            }

            // Triggered right before MSAL needs to access the cache.
            // Reload the cache from the persistent store in case it changed since the last access.
            private void BeforeAccessNotification(TokenCacheNotificationArgs args)
            {
                this.Load();
            }

            // Triggered right after MSAL accessed the cache.
            private void AfterAccessNotification(TokenCacheNotificationArgs args)
            {
                // if the access operation resulted in a cache update
                if (this.cache.HasStateChanged)
                {
                    this.Persist();
                }
            }

            public TokenCache GetMsalCacheInstance()
            {
                this.cache.SetBeforeAccess(this.BeforeAccessNotification);
                this.cache.SetAfterAccess(this.AfterAccessNotification);
                this.Load();

                return this.cache;
            }
        }
    }


