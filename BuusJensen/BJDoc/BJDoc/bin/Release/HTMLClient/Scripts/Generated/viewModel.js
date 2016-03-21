/// <reference path="data.js" />

(function (lightSwitchApplication) {

    var $Screen = msls.Screen,
        $defineScreen = msls._defineScreen,
        $DataServiceQuery = msls.DataServiceQuery,
        $toODataString = msls._toODataString,
        $defineShowScreen = msls._defineShowScreen;

    function AddEditBruger(parameters, dataWorkspace) {
        /// <summary>
        /// Represents the AddEditBruger screen.
        /// </summary>
        /// <param name="parameters" type="Array">
        /// An array of screen parameter values.
        /// </param>
        /// <param name="dataWorkspace" type="msls.application.DataWorkspace" optional="true">
        /// An existing data workspace for this screen to use. By default, a new data workspace is created.
        /// </param>
        /// <field name="tblBruger" type="msls.application.tblBruger">
        /// Gets or sets the tblBruger for this screen.
        /// </field>
        /// <field name="details" type="msls.application.AddEditBruger.Details">
        /// Gets the details for this screen.
        /// </field>
        if (!dataWorkspace) {
            dataWorkspace = new lightSwitchApplication.DataWorkspace();
        }
        $Screen.call(this, dataWorkspace, "AddEditBruger", parameters);
    }

    function BrowseBrugers(parameters, dataWorkspace) {
        /// <summary>
        /// Represents the BrowseBrugers screen.
        /// </summary>
        /// <param name="parameters" type="Array">
        /// An array of screen parameter values.
        /// </param>
        /// <param name="dataWorkspace" type="msls.application.DataWorkspace" optional="true">
        /// An existing data workspace for this screen to use. By default, a new data workspace is created.
        /// </param>
        /// <field name="tblBrugers" type="msls.VisualCollection" elementType="msls.application.tblBruger">
        /// Gets the tblBrugers for this screen.
        /// </field>
        /// <field name="details" type="msls.application.BrowseBrugers.Details">
        /// Gets the details for this screen.
        /// </field>
        if (!dataWorkspace) {
            dataWorkspace = new lightSwitchApplication.DataWorkspace();
        }
        $Screen.call(this, dataWorkspace, "BrowseBrugers", parameters);
    }

    function ViewBruger(parameters, dataWorkspace) {
        /// <summary>
        /// Represents the ViewBruger screen.
        /// </summary>
        /// <param name="parameters" type="Array">
        /// An array of screen parameter values.
        /// </param>
        /// <param name="dataWorkspace" type="msls.application.DataWorkspace" optional="true">
        /// An existing data workspace for this screen to use. By default, a new data workspace is created.
        /// </param>
        /// <field name="tblBruger" type="msls.application.tblBruger">
        /// Gets or sets the tblBruger for this screen.
        /// </field>
        /// <field name="details" type="msls.application.ViewBruger.Details">
        /// Gets the details for this screen.
        /// </field>
        if (!dataWorkspace) {
            dataWorkspace = new lightSwitchApplication.DataWorkspace();
        }
        $Screen.call(this, dataWorkspace, "ViewBruger", parameters);
    }

    function AddEditComputer(parameters, dataWorkspace) {
        /// <summary>
        /// Represents the AddEditComputer screen.
        /// </summary>
        /// <param name="parameters" type="Array">
        /// An array of screen parameter values.
        /// </param>
        /// <param name="dataWorkspace" type="msls.application.DataWorkspace" optional="true">
        /// An existing data workspace for this screen to use. By default, a new data workspace is created.
        /// </param>
        /// <field name="tblComputer" type="msls.application.tblComputer">
        /// Gets or sets the tblComputer for this screen.
        /// </field>
        /// <field name="details" type="msls.application.AddEditComputer.Details">
        /// Gets the details for this screen.
        /// </field>
        if (!dataWorkspace) {
            dataWorkspace = new lightSwitchApplication.DataWorkspace();
        }
        $Screen.call(this, dataWorkspace, "AddEditComputer", parameters);
    }

    function BrowseComputers(parameters, dataWorkspace) {
        /// <summary>
        /// Represents the BrowseComputers screen.
        /// </summary>
        /// <param name="parameters" type="Array">
        /// An array of screen parameter values.
        /// </param>
        /// <param name="dataWorkspace" type="msls.application.DataWorkspace" optional="true">
        /// An existing data workspace for this screen to use. By default, a new data workspace is created.
        /// </param>
        /// <field name="tblComputers" type="msls.VisualCollection" elementType="msls.application.tblComputer">
        /// Gets the tblComputers for this screen.
        /// </field>
        /// <field name="details" type="msls.application.BrowseComputers.Details">
        /// Gets the details for this screen.
        /// </field>
        if (!dataWorkspace) {
            dataWorkspace = new lightSwitchApplication.DataWorkspace();
        }
        $Screen.call(this, dataWorkspace, "BrowseComputers", parameters);
    }

    function ViewComputer(parameters, dataWorkspace) {
        /// <summary>
        /// Represents the ViewComputer screen.
        /// </summary>
        /// <param name="parameters" type="Array">
        /// An array of screen parameter values.
        /// </param>
        /// <param name="dataWorkspace" type="msls.application.DataWorkspace" optional="true">
        /// An existing data workspace for this screen to use. By default, a new data workspace is created.
        /// </param>
        /// <field name="tblComputer" type="msls.application.tblComputer">
        /// Gets or sets the tblComputer for this screen.
        /// </field>
        /// <field name="details" type="msls.application.ViewComputer.Details">
        /// Gets the details for this screen.
        /// </field>
        if (!dataWorkspace) {
            dataWorkspace = new lightSwitchApplication.DataWorkspace();
        }
        $Screen.call(this, dataWorkspace, "ViewComputer", parameters);
    }

    function AddEditHW_Type(parameters, dataWorkspace) {
        /// <summary>
        /// Represents the AddEditHW_Type screen.
        /// </summary>
        /// <param name="parameters" type="Array">
        /// An array of screen parameter values.
        /// </param>
        /// <param name="dataWorkspace" type="msls.application.DataWorkspace" optional="true">
        /// An existing data workspace for this screen to use. By default, a new data workspace is created.
        /// </param>
        /// <field name="tblHWtype" type="msls.application.tblHWtype">
        /// Gets or sets the tblHWtype for this screen.
        /// </field>
        /// <field name="details" type="msls.application.AddEditHW_Type.Details">
        /// Gets the details for this screen.
        /// </field>
        if (!dataWorkspace) {
            dataWorkspace = new lightSwitchApplication.DataWorkspace();
        }
        $Screen.call(this, dataWorkspace, "AddEditHW_Type", parameters);
    }

    function BrowseHW_Types(parameters, dataWorkspace) {
        /// <summary>
        /// Represents the BrowseHW_Types screen.
        /// </summary>
        /// <param name="parameters" type="Array">
        /// An array of screen parameter values.
        /// </param>
        /// <param name="dataWorkspace" type="msls.application.DataWorkspace" optional="true">
        /// An existing data workspace for this screen to use. By default, a new data workspace is created.
        /// </param>
        /// <field name="tblHWtypes" type="msls.VisualCollection" elementType="msls.application.tblHWtype">
        /// Gets the tblHWtypes for this screen.
        /// </field>
        /// <field name="details" type="msls.application.BrowseHW_Types.Details">
        /// Gets the details for this screen.
        /// </field>
        if (!dataWorkspace) {
            dataWorkspace = new lightSwitchApplication.DataWorkspace();
        }
        $Screen.call(this, dataWorkspace, "BrowseHW_Types", parameters);
    }

    function ViewHW_Type(parameters, dataWorkspace) {
        /// <summary>
        /// Represents the ViewHW_Type screen.
        /// </summary>
        /// <param name="parameters" type="Array">
        /// An array of screen parameter values.
        /// </param>
        /// <param name="dataWorkspace" type="msls.application.DataWorkspace" optional="true">
        /// An existing data workspace for this screen to use. By default, a new data workspace is created.
        /// </param>
        /// <field name="tblHWtype" type="msls.application.tblHWtype">
        /// Gets or sets the tblHWtype for this screen.
        /// </field>
        /// <field name="details" type="msls.application.ViewHW_Type.Details">
        /// Gets the details for this screen.
        /// </field>
        if (!dataWorkspace) {
            dataWorkspace = new lightSwitchApplication.DataWorkspace();
        }
        $Screen.call(this, dataWorkspace, "ViewHW_Type", parameters);
    }

    function AddEditHW(parameters, dataWorkspace) {
        /// <summary>
        /// Represents the AddEditHW screen.
        /// </summary>
        /// <param name="parameters" type="Array">
        /// An array of screen parameter values.
        /// </param>
        /// <param name="dataWorkspace" type="msls.application.DataWorkspace" optional="true">
        /// An existing data workspace for this screen to use. By default, a new data workspace is created.
        /// </param>
        /// <field name="tblHW" type="msls.application.tblHW">
        /// Gets or sets the tblHW for this screen.
        /// </field>
        /// <field name="details" type="msls.application.AddEditHW.Details">
        /// Gets the details for this screen.
        /// </field>
        if (!dataWorkspace) {
            dataWorkspace = new lightSwitchApplication.DataWorkspace();
        }
        $Screen.call(this, dataWorkspace, "AddEditHW", parameters);
    }

    function BrowseHWs(parameters, dataWorkspace) {
        /// <summary>
        /// Represents the BrowseHWs screen.
        /// </summary>
        /// <param name="parameters" type="Array">
        /// An array of screen parameter values.
        /// </param>
        /// <param name="dataWorkspace" type="msls.application.DataWorkspace" optional="true">
        /// An existing data workspace for this screen to use. By default, a new data workspace is created.
        /// </param>
        /// <field name="tblHWs" type="msls.VisualCollection" elementType="msls.application.tblHW">
        /// Gets the tblHWs for this screen.
        /// </field>
        /// <field name="details" type="msls.application.BrowseHWs.Details">
        /// Gets the details for this screen.
        /// </field>
        if (!dataWorkspace) {
            dataWorkspace = new lightSwitchApplication.DataWorkspace();
        }
        $Screen.call(this, dataWorkspace, "BrowseHWs", parameters);
    }

    function ViewHW(parameters, dataWorkspace) {
        /// <summary>
        /// Represents the ViewHW screen.
        /// </summary>
        /// <param name="parameters" type="Array">
        /// An array of screen parameter values.
        /// </param>
        /// <param name="dataWorkspace" type="msls.application.DataWorkspace" optional="true">
        /// An existing data workspace for this screen to use. By default, a new data workspace is created.
        /// </param>
        /// <field name="tblHW" type="msls.application.tblHW">
        /// Gets or sets the tblHW for this screen.
        /// </field>
        /// <field name="details" type="msls.application.ViewHW.Details">
        /// Gets the details for this screen.
        /// </field>
        if (!dataWorkspace) {
            dataWorkspace = new lightSwitchApplication.DataWorkspace();
        }
        $Screen.call(this, dataWorkspace, "ViewHW", parameters);
    }

    msls._addToNamespace("msls.application", {

        AddEditBruger: $defineScreen(AddEditBruger, [
            { name: "tblBruger", kind: "local", type: lightSwitchApplication.tblBruger }
        ], [
        ]),

        BrowseBrugers: $defineScreen(BrowseBrugers, [
            {
                name: "tblBrugers", kind: "collection", elementType: lightSwitchApplication.tblBruger,
                createQuery: function () {
                    return this.dataWorkspace.ApplicationData.tblBrugers;
                }
            }
        ], [
        ]),

        ViewBruger: $defineScreen(ViewBruger, [
            { name: "tblBruger", kind: "local", type: lightSwitchApplication.tblBruger }
        ], [
        ]),

        AddEditComputer: $defineScreen(AddEditComputer, [
            { name: "tblComputer", kind: "local", type: lightSwitchApplication.tblComputer }
        ], [
        ]),

        BrowseComputers: $defineScreen(BrowseComputers, [
            {
                name: "tblComputers", kind: "collection", elementType: lightSwitchApplication.tblComputer,
                createQuery: function () {
                    return this.dataWorkspace.ApplicationData.tblComputers.expand("tblHW").expand("tblBruger");
                }
            }
        ], [
        ]),

        ViewComputer: $defineScreen(ViewComputer, [
            { name: "tblComputer", kind: "local", type: lightSwitchApplication.tblComputer }
        ], [
        ]),

        AddEditHW_Type: $defineScreen(AddEditHW_Type, [
            { name: "tblHWtype", kind: "local", type: lightSwitchApplication.tblHWtype }
        ], [
        ]),

        BrowseHW_Types: $defineScreen(BrowseHW_Types, [
            {
                name: "tblHWtypes", kind: "collection", elementType: lightSwitchApplication.tblHWtype,
                createQuery: function () {
                    return this.dataWorkspace.ApplicationData.tblHWtypes;
                }
            }
        ], [
        ]),

        ViewHW_Type: $defineScreen(ViewHW_Type, [
            { name: "tblHWtype", kind: "local", type: lightSwitchApplication.tblHWtype }
        ], [
        ]),

        AddEditHW: $defineScreen(AddEditHW, [
            { name: "tblHW", kind: "local", type: lightSwitchApplication.tblHW }
        ], [
        ]),

        BrowseHWs: $defineScreen(BrowseHWs, [
            {
                name: "tblHWs", kind: "collection", elementType: lightSwitchApplication.tblHW,
                createQuery: function () {
                    return this.dataWorkspace.ApplicationData.tblHWs.expand("tblHWtype");
                }
            }
        ], [
        ]),

        ViewHW: $defineScreen(ViewHW, [
            { name: "tblHW", kind: "local", type: lightSwitchApplication.tblHW }
        ], [
        ]),

        showAddEditBruger: $defineShowScreen(function showAddEditBruger(tblBruger, options) {
            /// <summary>
            /// Asynchronously navigates forward to the AddEditBruger screen.
            /// </summary>
            /// <param name="options" optional="true">
            /// An object that provides one or more of the following options:<br/>- beforeShown: a function that is called after boundary behavior has been applied but before the screen is shown.<br/>+ Signature: beforeShown(screen)<br/>- afterClosed: a function that is called after boundary behavior has been applied and the screen has been closed.<br/>+ Signature: afterClosed(screen, action : msls.NavigateBackAction)
            /// </param>
            /// <returns type="WinJS.Promise" />
            var parameters = Array.prototype.slice.call(arguments, 0, 1);
            return lightSwitchApplication.showScreen("AddEditBruger", parameters, options);
        }),

        showBrowseBrugers: $defineShowScreen(function showBrowseBrugers(options) {
            /// <summary>
            /// Asynchronously navigates forward to the BrowseBrugers screen.
            /// </summary>
            /// <param name="options" optional="true">
            /// An object that provides one or more of the following options:<br/>- beforeShown: a function that is called after boundary behavior has been applied but before the screen is shown.<br/>+ Signature: beforeShown(screen)<br/>- afterClosed: a function that is called after boundary behavior has been applied and the screen has been closed.<br/>+ Signature: afterClosed(screen, action : msls.NavigateBackAction)
            /// </param>
            /// <returns type="WinJS.Promise" />
            var parameters = Array.prototype.slice.call(arguments, 0, 0);
            return lightSwitchApplication.showScreen("BrowseBrugers", parameters, options);
        }),

        showViewBruger: $defineShowScreen(function showViewBruger(tblBruger, options) {
            /// <summary>
            /// Asynchronously navigates forward to the ViewBruger screen.
            /// </summary>
            /// <param name="options" optional="true">
            /// An object that provides one or more of the following options:<br/>- beforeShown: a function that is called after boundary behavior has been applied but before the screen is shown.<br/>+ Signature: beforeShown(screen)<br/>- afterClosed: a function that is called after boundary behavior has been applied and the screen has been closed.<br/>+ Signature: afterClosed(screen, action : msls.NavigateBackAction)
            /// </param>
            /// <returns type="WinJS.Promise" />
            var parameters = Array.prototype.slice.call(arguments, 0, 1);
            return lightSwitchApplication.showScreen("ViewBruger", parameters, options);
        }),

        showAddEditComputer: $defineShowScreen(function showAddEditComputer(tblComputer, options) {
            /// <summary>
            /// Asynchronously navigates forward to the AddEditComputer screen.
            /// </summary>
            /// <param name="options" optional="true">
            /// An object that provides one or more of the following options:<br/>- beforeShown: a function that is called after boundary behavior has been applied but before the screen is shown.<br/>+ Signature: beforeShown(screen)<br/>- afterClosed: a function that is called after boundary behavior has been applied and the screen has been closed.<br/>+ Signature: afterClosed(screen, action : msls.NavigateBackAction)
            /// </param>
            /// <returns type="WinJS.Promise" />
            var parameters = Array.prototype.slice.call(arguments, 0, 1);
            return lightSwitchApplication.showScreen("AddEditComputer", parameters, options);
        }),

        showBrowseComputers: $defineShowScreen(function showBrowseComputers(options) {
            /// <summary>
            /// Asynchronously navigates forward to the BrowseComputers screen.
            /// </summary>
            /// <param name="options" optional="true">
            /// An object that provides one or more of the following options:<br/>- beforeShown: a function that is called after boundary behavior has been applied but before the screen is shown.<br/>+ Signature: beforeShown(screen)<br/>- afterClosed: a function that is called after boundary behavior has been applied and the screen has been closed.<br/>+ Signature: afterClosed(screen, action : msls.NavigateBackAction)
            /// </param>
            /// <returns type="WinJS.Promise" />
            var parameters = Array.prototype.slice.call(arguments, 0, 0);
            return lightSwitchApplication.showScreen("BrowseComputers", parameters, options);
        }),

        showViewComputer: $defineShowScreen(function showViewComputer(tblComputer, options) {
            /// <summary>
            /// Asynchronously navigates forward to the ViewComputer screen.
            /// </summary>
            /// <param name="options" optional="true">
            /// An object that provides one or more of the following options:<br/>- beforeShown: a function that is called after boundary behavior has been applied but before the screen is shown.<br/>+ Signature: beforeShown(screen)<br/>- afterClosed: a function that is called after boundary behavior has been applied and the screen has been closed.<br/>+ Signature: afterClosed(screen, action : msls.NavigateBackAction)
            /// </param>
            /// <returns type="WinJS.Promise" />
            var parameters = Array.prototype.slice.call(arguments, 0, 1);
            return lightSwitchApplication.showScreen("ViewComputer", parameters, options);
        }),

        showAddEditHW_Type: $defineShowScreen(function showAddEditHW_Type(tblHWtype, options) {
            /// <summary>
            /// Asynchronously navigates forward to the AddEditHW_Type screen.
            /// </summary>
            /// <param name="options" optional="true">
            /// An object that provides one or more of the following options:<br/>- beforeShown: a function that is called after boundary behavior has been applied but before the screen is shown.<br/>+ Signature: beforeShown(screen)<br/>- afterClosed: a function that is called after boundary behavior has been applied and the screen has been closed.<br/>+ Signature: afterClosed(screen, action : msls.NavigateBackAction)
            /// </param>
            /// <returns type="WinJS.Promise" />
            var parameters = Array.prototype.slice.call(arguments, 0, 1);
            return lightSwitchApplication.showScreen("AddEditHW_Type", parameters, options);
        }),

        showBrowseHW_Types: $defineShowScreen(function showBrowseHW_Types(options) {
            /// <summary>
            /// Asynchronously navigates forward to the BrowseHW_Types screen.
            /// </summary>
            /// <param name="options" optional="true">
            /// An object that provides one or more of the following options:<br/>- beforeShown: a function that is called after boundary behavior has been applied but before the screen is shown.<br/>+ Signature: beforeShown(screen)<br/>- afterClosed: a function that is called after boundary behavior has been applied and the screen has been closed.<br/>+ Signature: afterClosed(screen, action : msls.NavigateBackAction)
            /// </param>
            /// <returns type="WinJS.Promise" />
            var parameters = Array.prototype.slice.call(arguments, 0, 0);
            return lightSwitchApplication.showScreen("BrowseHW_Types", parameters, options);
        }),

        showViewHW_Type: $defineShowScreen(function showViewHW_Type(tblHWtype, options) {
            /// <summary>
            /// Asynchronously navigates forward to the ViewHW_Type screen.
            /// </summary>
            /// <param name="options" optional="true">
            /// An object that provides one or more of the following options:<br/>- beforeShown: a function that is called after boundary behavior has been applied but before the screen is shown.<br/>+ Signature: beforeShown(screen)<br/>- afterClosed: a function that is called after boundary behavior has been applied and the screen has been closed.<br/>+ Signature: afterClosed(screen, action : msls.NavigateBackAction)
            /// </param>
            /// <returns type="WinJS.Promise" />
            var parameters = Array.prototype.slice.call(arguments, 0, 1);
            return lightSwitchApplication.showScreen("ViewHW_Type", parameters, options);
        }),

        showAddEditHW: $defineShowScreen(function showAddEditHW(tblHW, options) {
            /// <summary>
            /// Asynchronously navigates forward to the AddEditHW screen.
            /// </summary>
            /// <param name="options" optional="true">
            /// An object that provides one or more of the following options:<br/>- beforeShown: a function that is called after boundary behavior has been applied but before the screen is shown.<br/>+ Signature: beforeShown(screen)<br/>- afterClosed: a function that is called after boundary behavior has been applied and the screen has been closed.<br/>+ Signature: afterClosed(screen, action : msls.NavigateBackAction)
            /// </param>
            /// <returns type="WinJS.Promise" />
            var parameters = Array.prototype.slice.call(arguments, 0, 1);
            return lightSwitchApplication.showScreen("AddEditHW", parameters, options);
        }),

        showBrowseHWs: $defineShowScreen(function showBrowseHWs(options) {
            /// <summary>
            /// Asynchronously navigates forward to the BrowseHWs screen.
            /// </summary>
            /// <param name="options" optional="true">
            /// An object that provides one or more of the following options:<br/>- beforeShown: a function that is called after boundary behavior has been applied but before the screen is shown.<br/>+ Signature: beforeShown(screen)<br/>- afterClosed: a function that is called after boundary behavior has been applied and the screen has been closed.<br/>+ Signature: afterClosed(screen, action : msls.NavigateBackAction)
            /// </param>
            /// <returns type="WinJS.Promise" />
            var parameters = Array.prototype.slice.call(arguments, 0, 0);
            return lightSwitchApplication.showScreen("BrowseHWs", parameters, options);
        }),

        showViewHW: $defineShowScreen(function showViewHW(tblHW, options) {
            /// <summary>
            /// Asynchronously navigates forward to the ViewHW screen.
            /// </summary>
            /// <param name="options" optional="true">
            /// An object that provides one or more of the following options:<br/>- beforeShown: a function that is called after boundary behavior has been applied but before the screen is shown.<br/>+ Signature: beforeShown(screen)<br/>- afterClosed: a function that is called after boundary behavior has been applied and the screen has been closed.<br/>+ Signature: afterClosed(screen, action : msls.NavigateBackAction)
            /// </param>
            /// <returns type="WinJS.Promise" />
            var parameters = Array.prototype.slice.call(arguments, 0, 1);
            return lightSwitchApplication.showScreen("ViewHW", parameters, options);
        })

    });

}(msls.application));
