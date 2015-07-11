/// <reference path="data.js" />

(function (lightSwitchApplication) {

    var $Screen = msls.Screen,
        $defineScreen = msls._defineScreen,
        $DataServiceQuery = msls.DataServiceQuery,
        $toODataString = msls._toODataString,
        $defineShowScreen = msls._defineShowScreen;

    function AddEditBetalinger(parameters, dataWorkspace) {
        /// <summary>
        /// Represents the AddEditBetalinger screen.
        /// </summary>
        /// <param name="parameters" type="Array">
        /// An array of screen parameter values.
        /// </param>
        /// <param name="dataWorkspace" type="msls.application.DataWorkspace" optional="true">
        /// An existing data workspace for this screen to use. By default, a new data workspace is created.
        /// </param>
        /// <field name="tblFikBetaling" type="msls.application.tblFikBetaling">
        /// Gets or sets the tblFikBetaling for this screen.
        /// </field>
        /// <field name="details" type="msls.application.AddEditBetalinger.Details">
        /// Gets the details for this screen.
        /// </field>
        if (!dataWorkspace) {
            dataWorkspace = new lightSwitchApplication.DataWorkspace();
        }
        $Screen.call(this, dataWorkspace, "AddEditBetalinger", parameters);
    }

    function BrowseBetalingers(parameters, dataWorkspace) {
        /// <summary>
        /// Represents the BrowseBetalingers screen.
        /// </summary>
        /// <param name="parameters" type="Array">
        /// An array of screen parameter values.
        /// </param>
        /// <param name="dataWorkspace" type="msls.application.DataWorkspace" optional="true">
        /// An existing data workspace for this screen to use. By default, a new data workspace is created.
        /// </param>
        /// <field name="tblFikBetalings" type="msls.VisualCollection" elementType="msls.application.tblFikBetaling">
        /// Gets the tblFikBetalings for this screen.
        /// </field>
        /// <field name="details" type="msls.application.BrowseBetalingers.Details">
        /// Gets the details for this screen.
        /// </field>
        if (!dataWorkspace) {
            dataWorkspace = new lightSwitchApplication.DataWorkspace();
        }
        $Screen.call(this, dataWorkspace, "BrowseBetalingers", parameters);
    }

    function ViewBetalinger(parameters, dataWorkspace) {
        /// <summary>
        /// Represents the ViewBetalinger screen.
        /// </summary>
        /// <param name="parameters" type="Array">
        /// An array of screen parameter values.
        /// </param>
        /// <param name="dataWorkspace" type="msls.application.DataWorkspace" optional="true">
        /// An existing data workspace for this screen to use. By default, a new data workspace is created.
        /// </param>
        /// <field name="tblFikBetaling" type="msls.application.tblFikBetaling">
        /// Gets or sets the tblFikBetaling for this screen.
        /// </field>
        /// <field name="details" type="msls.application.ViewBetalinger.Details">
        /// Gets the details for this screen.
        /// </field>
        if (!dataWorkspace) {
            dataWorkspace = new lightSwitchApplication.DataWorkspace();
        }
        $Screen.call(this, dataWorkspace, "ViewBetalinger", parameters);
    }

    function Medlemmer(parameters, dataWorkspace) {
        /// <summary>
        /// Represents the Medlemmer screen.
        /// </summary>
        /// <param name="parameters" type="Array">
        /// An array of screen parameter values.
        /// </param>
        /// <param name="dataWorkspace" type="msls.application.DataWorkspace" optional="true">
        /// An existing data workspace for this screen to use. By default, a new data workspace is created.
        /// </param>
        /// <field name="tblMedlems" type="msls.VisualCollection" elementType="msls.application.tblMedlem">
        /// Gets the tblMedlems for this screen.
        /// </field>
        /// <field name="details" type="msls.application.Medlemmer.Details">
        /// Gets the details for this screen.
        /// </field>
        if (!dataWorkspace) {
            dataWorkspace = new lightSwitchApplication.DataWorkspace();
        }
        $Screen.call(this, dataWorkspace, "Medlemmer", parameters);
    }

    function AddEditProjekt(parameters, dataWorkspace) {
        /// <summary>
        /// Represents the AddEditProjekt screen.
        /// </summary>
        /// <param name="parameters" type="Array">
        /// An array of screen parameter values.
        /// </param>
        /// <param name="dataWorkspace" type="msls.application.DataWorkspace" optional="true">
        /// An existing data workspace for this screen to use. By default, a new data workspace is created.
        /// </param>
        /// <field name="tblProjekt" type="msls.application.tblProjekt">
        /// Gets or sets the tblProjekt for this screen.
        /// </field>
        /// <field name="details" type="msls.application.AddEditProjekt.Details">
        /// Gets the details for this screen.
        /// </field>
        if (!dataWorkspace) {
            dataWorkspace = new lightSwitchApplication.DataWorkspace();
        }
        $Screen.call(this, dataWorkspace, "AddEditProjekt", parameters);
    }

    function BrowseProjekts(parameters, dataWorkspace) {
        /// <summary>
        /// Represents the BrowseProjekts screen.
        /// </summary>
        /// <param name="parameters" type="Array">
        /// An array of screen parameter values.
        /// </param>
        /// <param name="dataWorkspace" type="msls.application.DataWorkspace" optional="true">
        /// An existing data workspace for this screen to use. By default, a new data workspace is created.
        /// </param>
        /// <field name="tblProjekts" type="msls.VisualCollection" elementType="msls.application.tblProjekt">
        /// Gets the tblProjekts for this screen.
        /// </field>
        /// <field name="details" type="msls.application.BrowseProjekts.Details">
        /// Gets the details for this screen.
        /// </field>
        if (!dataWorkspace) {
            dataWorkspace = new lightSwitchApplication.DataWorkspace();
        }
        $Screen.call(this, dataWorkspace, "BrowseProjekts", parameters);
    }

    function ViewProjekt(parameters, dataWorkspace) {
        /// <summary>
        /// Represents the ViewProjekt screen.
        /// </summary>
        /// <param name="parameters" type="Array">
        /// An array of screen parameter values.
        /// </param>
        /// <param name="dataWorkspace" type="msls.application.DataWorkspace" optional="true">
        /// An existing data workspace for this screen to use. By default, a new data workspace is created.
        /// </param>
        /// <field name="tblProjekt" type="msls.application.tblProjekt">
        /// Gets or sets the tblProjekt for this screen.
        /// </field>
        /// <field name="details" type="msls.application.ViewProjekt.Details">
        /// Gets the details for this screen.
        /// </field>
        if (!dataWorkspace) {
            dataWorkspace = new lightSwitchApplication.DataWorkspace();
        }
        $Screen.call(this, dataWorkspace, "ViewProjekt", parameters);
    }

    msls._addToNamespace("msls.application", {

        AddEditBetalinger: $defineScreen(AddEditBetalinger, [
            { name: "tblFikBetaling", kind: "local", type: lightSwitchApplication.tblFikBetaling }
        ], [
        ]),

        BrowseBetalingers: $defineScreen(BrowseBetalingers, [
            {
                name: "tblFikBetalings", kind: "collection", elementType: lightSwitchApplication.tblFikBetaling,
                createQuery: function () {
                    return this.dataWorkspace.ApplicationData.tblFikBetalings.expand("tblProjekt");
                }
            }
        ], [
        ]),

        ViewBetalinger: $defineScreen(ViewBetalinger, [
            { name: "tblFikBetaling", kind: "local", type: lightSwitchApplication.tblFikBetaling }
        ], [
        ]),

        Medlemmer: $defineScreen(Medlemmer, [
            {
                name: "tblMedlems", kind: "collection", elementType: lightSwitchApplication.tblMedlem,
                createQuery: function () {
                    return this.dataWorkspace.dbPuls3060MedlemData.tblMedlems.filter("(Status ne null) and (Status gt 0)").orderBy("Kaldenavn");
                }
            }
        ], [
        ]),

        AddEditProjekt: $defineScreen(AddEditProjekt, [
            { name: "tblProjekt", kind: "local", type: lightSwitchApplication.tblProjekt }
        ], [
        ]),

        BrowseProjekts: $defineScreen(BrowseProjekts, [
            {
                name: "tblProjekts", kind: "collection", elementType: lightSwitchApplication.tblProjekt,
                createQuery: function () {
                    return this.dataWorkspace.ApplicationData.tblProjekts;
                }
            }
        ], [
        ]),

        ViewProjekt: $defineScreen(ViewProjekt, [
            { name: "tblProjekt", kind: "local", type: lightSwitchApplication.tblProjekt }
        ], [
        ]),

        showAddEditBetalinger: $defineShowScreen(function showAddEditBetalinger(tblFikBetaling, options) {
            /// <summary>
            /// Asynchronously navigates forward to the AddEditBetalinger screen.
            /// </summary>
            /// <param name="options" optional="true">
            /// An object that provides one or more of the following options:<br/>- beforeShown: a function that is called after boundary behavior has been applied but before the screen is shown.<br/>+ Signature: beforeShown(screen)<br/>- afterClosed: a function that is called after boundary behavior has been applied and the screen has been closed.<br/>+ Signature: afterClosed(screen, action : msls.NavigateBackAction)
            /// </param>
            /// <returns type="WinJS.Promise" />
            var parameters = Array.prototype.slice.call(arguments, 0, 1);
            return lightSwitchApplication.showScreen("AddEditBetalinger", parameters, options);
        }),

        showBrowseBetalingers: $defineShowScreen(function showBrowseBetalingers(options) {
            /// <summary>
            /// Asynchronously navigates forward to the BrowseBetalingers screen.
            /// </summary>
            /// <param name="options" optional="true">
            /// An object that provides one or more of the following options:<br/>- beforeShown: a function that is called after boundary behavior has been applied but before the screen is shown.<br/>+ Signature: beforeShown(screen)<br/>- afterClosed: a function that is called after boundary behavior has been applied and the screen has been closed.<br/>+ Signature: afterClosed(screen, action : msls.NavigateBackAction)
            /// </param>
            /// <returns type="WinJS.Promise" />
            var parameters = Array.prototype.slice.call(arguments, 0, 0);
            return lightSwitchApplication.showScreen("BrowseBetalingers", parameters, options);
        }),

        showViewBetalinger: $defineShowScreen(function showViewBetalinger(tblFikBetaling, options) {
            /// <summary>
            /// Asynchronously navigates forward to the ViewBetalinger screen.
            /// </summary>
            /// <param name="options" optional="true">
            /// An object that provides one or more of the following options:<br/>- beforeShown: a function that is called after boundary behavior has been applied but before the screen is shown.<br/>+ Signature: beforeShown(screen)<br/>- afterClosed: a function that is called after boundary behavior has been applied and the screen has been closed.<br/>+ Signature: afterClosed(screen, action : msls.NavigateBackAction)
            /// </param>
            /// <returns type="WinJS.Promise" />
            var parameters = Array.prototype.slice.call(arguments, 0, 1);
            return lightSwitchApplication.showScreen("ViewBetalinger", parameters, options);
        }),

        showMedlemmer: $defineShowScreen(function showMedlemmer(options) {
            /// <summary>
            /// Asynchronously navigates forward to the Medlemmer screen.
            /// </summary>
            /// <param name="options" optional="true">
            /// An object that provides one or more of the following options:<br/>- beforeShown: a function that is called after boundary behavior has been applied but before the screen is shown.<br/>+ Signature: beforeShown(screen)<br/>- afterClosed: a function that is called after boundary behavior has been applied and the screen has been closed.<br/>+ Signature: afterClosed(screen, action : msls.NavigateBackAction)
            /// </param>
            /// <returns type="WinJS.Promise" />
            var parameters = Array.prototype.slice.call(arguments, 0, 0);
            return lightSwitchApplication.showScreen("Medlemmer", parameters, options);
        }),

        showAddEditProjekt: $defineShowScreen(function showAddEditProjekt(tblProjekt, options) {
            /// <summary>
            /// Asynchronously navigates forward to the AddEditProjekt screen.
            /// </summary>
            /// <param name="options" optional="true">
            /// An object that provides one or more of the following options:<br/>- beforeShown: a function that is called after boundary behavior has been applied but before the screen is shown.<br/>+ Signature: beforeShown(screen)<br/>- afterClosed: a function that is called after boundary behavior has been applied and the screen has been closed.<br/>+ Signature: afterClosed(screen, action : msls.NavigateBackAction)
            /// </param>
            /// <returns type="WinJS.Promise" />
            var parameters = Array.prototype.slice.call(arguments, 0, 1);
            return lightSwitchApplication.showScreen("AddEditProjekt", parameters, options);
        }),

        showBrowseProjekts: $defineShowScreen(function showBrowseProjekts(options) {
            /// <summary>
            /// Asynchronously navigates forward to the BrowseProjekts screen.
            /// </summary>
            /// <param name="options" optional="true">
            /// An object that provides one or more of the following options:<br/>- beforeShown: a function that is called after boundary behavior has been applied but before the screen is shown.<br/>+ Signature: beforeShown(screen)<br/>- afterClosed: a function that is called after boundary behavior has been applied and the screen has been closed.<br/>+ Signature: afterClosed(screen, action : msls.NavigateBackAction)
            /// </param>
            /// <returns type="WinJS.Promise" />
            var parameters = Array.prototype.slice.call(arguments, 0, 0);
            return lightSwitchApplication.showScreen("BrowseProjekts", parameters, options);
        }),

        showViewProjekt: $defineShowScreen(function showViewProjekt(tblProjekt, options) {
            /// <summary>
            /// Asynchronously navigates forward to the ViewProjekt screen.
            /// </summary>
            /// <param name="options" optional="true">
            /// An object that provides one or more of the following options:<br/>- beforeShown: a function that is called after boundary behavior has been applied but before the screen is shown.<br/>+ Signature: beforeShown(screen)<br/>- afterClosed: a function that is called after boundary behavior has been applied and the screen has been closed.<br/>+ Signature: afterClosed(screen, action : msls.NavigateBackAction)
            /// </param>
            /// <returns type="WinJS.Promise" />
            var parameters = Array.prototype.slice.call(arguments, 0, 1);
            return lightSwitchApplication.showScreen("ViewProjekt", parameters, options);
        })

    });

}(msls.application));
