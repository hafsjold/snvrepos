using System;

namespace SPDevtools
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public sealed class TargetListAttribute : Attribute
    {
        public TargetListAttribute(string id)
        {
            this.m_id = id;
        }

        private string m_id = String.Empty;
        public string Id
        {
            get { return this.m_id; }
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public sealed class TargetContentTypeAttribute : Attribute
    {
        public TargetContentTypeAttribute(string featureId, string contentTypeId)
        {
            this.m_featureId = featureId;
            this.m_contentTypeId = contentTypeId;
        }

        private string m_featureId = String.Empty;
        public string FeatureId
        {
            get { return this.m_featureId; }
        }

        private string m_contentTypeId = String.Empty;
        public string ContentTypeId
        {
            get { return this.m_contentTypeId; }
        }
    }
}
