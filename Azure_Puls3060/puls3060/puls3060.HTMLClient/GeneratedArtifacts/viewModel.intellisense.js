/// <reference path="viewModel.js" />

(function (lightSwitchApplication) {

    var $element = document.createElement("div");

    lightSwitchApplication.AddEditBetalinger.prototype._$contentItems = {
        Tabs: {
            _$class: msls.ContentItem,
            _$name: "Tabs",
            _$parentName: "RootContentItem",
            screen: lightSwitchApplication.AddEditBetalinger
        },
        Details: {
            _$class: msls.ContentItem,
            _$name: "Details",
            _$parentName: "Tabs",
            screen: lightSwitchApplication.AddEditBetalinger,
            data: lightSwitchApplication.AddEditBetalinger,
            value: lightSwitchApplication.AddEditBetalinger
        },
        columns: {
            _$class: msls.ContentItem,
            _$name: "columns",
            _$parentName: "Details",
            screen: lightSwitchApplication.AddEditBetalinger,
            data: lightSwitchApplication.AddEditBetalinger,
            value: lightSwitchApplication.tblFikBetaling
        },
        left: {
            _$class: msls.ContentItem,
            _$name: "left",
            _$parentName: "columns",
            screen: lightSwitchApplication.AddEditBetalinger,
            data: lightSwitchApplication.tblFikBetaling,
            value: lightSwitchApplication.tblFikBetaling
        },
        tblProjekt: {
            _$class: msls.ContentItem,
            _$name: "tblProjekt",
            _$parentName: "left",
            screen: lightSwitchApplication.AddEditBetalinger,
            data: lightSwitchApplication.tblFikBetaling,
            value: lightSwitchApplication.tblProjekt
        },
        RowTemplate: {
            _$class: msls.ContentItem,
            _$name: "RowTemplate",
            _$parentName: "tblProjekt",
            screen: lightSwitchApplication.AddEditBetalinger,
            data: lightSwitchApplication.tblProjekt,
            value: lightSwitchApplication.tblProjekt
        },
        tblMedlem: {
            _$class: msls.ContentItem,
            _$name: "tblMedlem",
            _$parentName: "left",
            screen: lightSwitchApplication.AddEditBetalinger,
            data: lightSwitchApplication.tblFikBetaling,
            value: lightSwitchApplication.tblMedlem
        },
        RowTemplate1: {
            _$class: msls.ContentItem,
            _$name: "RowTemplate1",
            _$parentName: "tblMedlem",
            screen: lightSwitchApplication.AddEditBetalinger,
            data: lightSwitchApplication.tblMedlem,
            value: lightSwitchApplication.tblMedlem
        },
        Belob: {
            _$class: msls.ContentItem,
            _$name: "Belob",
            _$parentName: "left",
            screen: lightSwitchApplication.AddEditBetalinger,
            data: lightSwitchApplication.tblFikBetaling,
            value: String
        },
        BetalingsDato: {
            _$class: msls.ContentItem,
            _$name: "BetalingsDato",
            _$parentName: "left",
            screen: lightSwitchApplication.AddEditBetalinger,
            data: lightSwitchApplication.tblFikBetaling,
            value: Date
        },
        FIKnr: {
            _$class: msls.ContentItem,
            _$name: "FIKnr",
            _$parentName: "left",
            screen: lightSwitchApplication.AddEditBetalinger,
            data: lightSwitchApplication.tblFikBetaling,
            value: String
        },
        right: {
            _$class: msls.ContentItem,
            _$name: "right",
            _$parentName: "columns",
            screen: lightSwitchApplication.AddEditBetalinger,
            data: lightSwitchApplication.tblFikBetaling,
            value: lightSwitchApplication.tblFikBetaling
        },
        Popups: {
            _$class: msls.ContentItem,
            _$name: "Popups",
            _$parentName: "RootContentItem",
            screen: lightSwitchApplication.AddEditBetalinger
        }
    };

    msls._addEntryPoints(lightSwitchApplication.AddEditBetalinger, {
        /// <field>
        /// Called when a new AddEditBetalinger screen is created.
        /// <br/>created(msls.application.AddEditBetalinger screen)
        /// </field>
        created: [lightSwitchApplication.AddEditBetalinger],
        /// <field>
        /// Called before changes on an active AddEditBetalinger screen are applied.
        /// <br/>beforeApplyChanges(msls.application.AddEditBetalinger screen)
        /// </field>
        beforeApplyChanges: [lightSwitchApplication.AddEditBetalinger],
        /// <field>
        /// Called after the Details content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        Details_postRender: [$element, function () { return new lightSwitchApplication.AddEditBetalinger().findContentItem("Details"); }],
        /// <field>
        /// Called after the columns content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        columns_postRender: [$element, function () { return new lightSwitchApplication.AddEditBetalinger().findContentItem("columns"); }],
        /// <field>
        /// Called after the left content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        left_postRender: [$element, function () { return new lightSwitchApplication.AddEditBetalinger().findContentItem("left"); }],
        /// <field>
        /// Called after the tblProjekt content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        tblProjekt_postRender: [$element, function () { return new lightSwitchApplication.AddEditBetalinger().findContentItem("tblProjekt"); }],
        /// <field>
        /// Called after the RowTemplate content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        RowTemplate_postRender: [$element, function () { return new lightSwitchApplication.AddEditBetalinger().findContentItem("RowTemplate"); }],
        /// <field>
        /// Called after the tblMedlem content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        tblMedlem_postRender: [$element, function () { return new lightSwitchApplication.AddEditBetalinger().findContentItem("tblMedlem"); }],
        /// <field>
        /// Called after the RowTemplate1 content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        RowTemplate1_postRender: [$element, function () { return new lightSwitchApplication.AddEditBetalinger().findContentItem("RowTemplate1"); }],
        /// <field>
        /// Called after the Belob content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        Belob_postRender: [$element, function () { return new lightSwitchApplication.AddEditBetalinger().findContentItem("Belob"); }],
        /// <field>
        /// Called after the BetalingsDato content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        BetalingsDato_postRender: [$element, function () { return new lightSwitchApplication.AddEditBetalinger().findContentItem("BetalingsDato"); }],
        /// <field>
        /// Called after the FIKnr content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        FIKnr_postRender: [$element, function () { return new lightSwitchApplication.AddEditBetalinger().findContentItem("FIKnr"); }],
        /// <field>
        /// Called after the right content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        right_postRender: [$element, function () { return new lightSwitchApplication.AddEditBetalinger().findContentItem("right"); }]
    });

    lightSwitchApplication.BrowseBetalingers.prototype._$contentItems = {
        Tabs: {
            _$class: msls.ContentItem,
            _$name: "Tabs",
            _$parentName: "RootContentItem",
            screen: lightSwitchApplication.BrowseBetalingers
        },
        BetalingerList: {
            _$class: msls.ContentItem,
            _$name: "BetalingerList",
            _$parentName: "Tabs",
            screen: lightSwitchApplication.BrowseBetalingers,
            data: lightSwitchApplication.BrowseBetalingers,
            value: lightSwitchApplication.BrowseBetalingers
        },
        tblFikBetalings: {
            _$class: msls.ContentItem,
            _$name: "tblFikBetalings",
            _$parentName: "BetalingerList",
            screen: lightSwitchApplication.BrowseBetalingers,
            data: lightSwitchApplication.BrowseBetalingers,
            value: {
                _$class: msls.VisualCollection,
                screen: lightSwitchApplication.BrowseBetalingers,
                _$entry: {
                    elementType: lightSwitchApplication.tblFikBetaling
                }
            }
        },
        rows: {
            _$class: msls.ContentItem,
            _$name: "rows",
            _$parentName: "tblFikBetalings",
            screen: lightSwitchApplication.BrowseBetalingers,
            data: lightSwitchApplication.tblFikBetaling,
            value: lightSwitchApplication.tblFikBetaling
        },
        tblProjekt: {
            _$class: msls.ContentItem,
            _$name: "tblProjekt",
            _$parentName: "rows",
            screen: lightSwitchApplication.BrowseBetalingers,
            data: lightSwitchApplication.tblFikBetaling,
            value: lightSwitchApplication.tblProjekt
        },
        tblMedlem: {
            _$class: msls.ContentItem,
            _$name: "tblMedlem",
            _$parentName: "rows",
            screen: lightSwitchApplication.BrowseBetalingers,
            data: lightSwitchApplication.tblFikBetaling,
            value: lightSwitchApplication.tblMedlem
        },
        Belob: {
            _$class: msls.ContentItem,
            _$name: "Belob",
            _$parentName: "rows",
            screen: lightSwitchApplication.BrowseBetalingers,
            data: lightSwitchApplication.tblFikBetaling,
            value: String
        },
        BetalingsDato: {
            _$class: msls.ContentItem,
            _$name: "BetalingsDato",
            _$parentName: "rows",
            screen: lightSwitchApplication.BrowseBetalingers,
            data: lightSwitchApplication.tblFikBetaling,
            value: Date
        },
        FIKnr: {
            _$class: msls.ContentItem,
            _$name: "FIKnr",
            _$parentName: "rows",
            screen: lightSwitchApplication.BrowseBetalingers,
            data: lightSwitchApplication.tblFikBetaling,
            value: String
        },
        Popups: {
            _$class: msls.ContentItem,
            _$name: "Popups",
            _$parentName: "RootContentItem",
            screen: lightSwitchApplication.BrowseBetalingers
        }
    };

    msls._addEntryPoints(lightSwitchApplication.BrowseBetalingers, {
        /// <field>
        /// Called when a new BrowseBetalingers screen is created.
        /// <br/>created(msls.application.BrowseBetalingers screen)
        /// </field>
        created: [lightSwitchApplication.BrowseBetalingers],
        /// <field>
        /// Called before changes on an active BrowseBetalingers screen are applied.
        /// <br/>beforeApplyChanges(msls.application.BrowseBetalingers screen)
        /// </field>
        beforeApplyChanges: [lightSwitchApplication.BrowseBetalingers],
        /// <field>
        /// Called after the BetalingerList content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        BetalingerList_postRender: [$element, function () { return new lightSwitchApplication.BrowseBetalingers().findContentItem("BetalingerList"); }],
        /// <field>
        /// Called after the tblFikBetalings content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        tblFikBetalings_postRender: [$element, function () { return new lightSwitchApplication.BrowseBetalingers().findContentItem("tblFikBetalings"); }],
        /// <field>
        /// Called after the rows content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        rows_postRender: [$element, function () { return new lightSwitchApplication.BrowseBetalingers().findContentItem("rows"); }],
        /// <field>
        /// Called after the tblProjekt content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        tblProjekt_postRender: [$element, function () { return new lightSwitchApplication.BrowseBetalingers().findContentItem("tblProjekt"); }],
        /// <field>
        /// Called after the tblMedlem content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        tblMedlem_postRender: [$element, function () { return new lightSwitchApplication.BrowseBetalingers().findContentItem("tblMedlem"); }],
        /// <field>
        /// Called after the Belob content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        Belob_postRender: [$element, function () { return new lightSwitchApplication.BrowseBetalingers().findContentItem("Belob"); }],
        /// <field>
        /// Called after the BetalingsDato content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        BetalingsDato_postRender: [$element, function () { return new lightSwitchApplication.BrowseBetalingers().findContentItem("BetalingsDato"); }],
        /// <field>
        /// Called after the FIKnr content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        FIKnr_postRender: [$element, function () { return new lightSwitchApplication.BrowseBetalingers().findContentItem("FIKnr"); }]
    });

    lightSwitchApplication.ViewBetalinger.prototype._$contentItems = {
        Tabs: {
            _$class: msls.ContentItem,
            _$name: "Tabs",
            _$parentName: "RootContentItem",
            screen: lightSwitchApplication.ViewBetalinger
        },
        Details: {
            _$class: msls.ContentItem,
            _$name: "Details",
            _$parentName: "Tabs",
            screen: lightSwitchApplication.ViewBetalinger,
            data: lightSwitchApplication.ViewBetalinger,
            value: lightSwitchApplication.ViewBetalinger
        },
        columns: {
            _$class: msls.ContentItem,
            _$name: "columns",
            _$parentName: "Details",
            screen: lightSwitchApplication.ViewBetalinger,
            data: lightSwitchApplication.ViewBetalinger,
            value: lightSwitchApplication.tblFikBetaling
        },
        left: {
            _$class: msls.ContentItem,
            _$name: "left",
            _$parentName: "columns",
            screen: lightSwitchApplication.ViewBetalinger,
            data: lightSwitchApplication.tblFikBetaling,
            value: lightSwitchApplication.tblFikBetaling
        },
        Id: {
            _$class: msls.ContentItem,
            _$name: "Id",
            _$parentName: "left",
            screen: lightSwitchApplication.ViewBetalinger,
            data: lightSwitchApplication.tblFikBetaling,
            value: Number
        },
        tblProjekt: {
            _$class: msls.ContentItem,
            _$name: "tblProjekt",
            _$parentName: "left",
            screen: lightSwitchApplication.ViewBetalinger,
            data: lightSwitchApplication.tblFikBetaling,
            value: lightSwitchApplication.tblProjekt
        },
        tblMedlem_Nr: {
            _$class: msls.ContentItem,
            _$name: "tblMedlem_Nr",
            _$parentName: "left",
            screen: lightSwitchApplication.ViewBetalinger,
            data: lightSwitchApplication.tblFikBetaling,
            value: Number
        },
        tblMedlem: {
            _$class: msls.ContentItem,
            _$name: "tblMedlem",
            _$parentName: "left",
            screen: lightSwitchApplication.ViewBetalinger,
            data: lightSwitchApplication.tblFikBetaling,
            value: lightSwitchApplication.tblMedlem
        },
        Belob: {
            _$class: msls.ContentItem,
            _$name: "Belob",
            _$parentName: "left",
            screen: lightSwitchApplication.ViewBetalinger,
            data: lightSwitchApplication.tblFikBetaling,
            value: String
        },
        BetalingsDato: {
            _$class: msls.ContentItem,
            _$name: "BetalingsDato",
            _$parentName: "left",
            screen: lightSwitchApplication.ViewBetalinger,
            data: lightSwitchApplication.tblFikBetaling,
            value: Date
        },
        FIKnr: {
            _$class: msls.ContentItem,
            _$name: "FIKnr",
            _$parentName: "left",
            screen: lightSwitchApplication.ViewBetalinger,
            data: lightSwitchApplication.tblFikBetaling,
            value: String
        },
        right: {
            _$class: msls.ContentItem,
            _$name: "right",
            _$parentName: "columns",
            screen: lightSwitchApplication.ViewBetalinger,
            data: lightSwitchApplication.tblFikBetaling,
            value: lightSwitchApplication.tblFikBetaling
        },
        Popups: {
            _$class: msls.ContentItem,
            _$name: "Popups",
            _$parentName: "RootContentItem",
            screen: lightSwitchApplication.ViewBetalinger
        }
    };

    msls._addEntryPoints(lightSwitchApplication.ViewBetalinger, {
        /// <field>
        /// Called when a new ViewBetalinger screen is created.
        /// <br/>created(msls.application.ViewBetalinger screen)
        /// </field>
        created: [lightSwitchApplication.ViewBetalinger],
        /// <field>
        /// Called before changes on an active ViewBetalinger screen are applied.
        /// <br/>beforeApplyChanges(msls.application.ViewBetalinger screen)
        /// </field>
        beforeApplyChanges: [lightSwitchApplication.ViewBetalinger],
        /// <field>
        /// Called after the Details content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        Details_postRender: [$element, function () { return new lightSwitchApplication.ViewBetalinger().findContentItem("Details"); }],
        /// <field>
        /// Called after the columns content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        columns_postRender: [$element, function () { return new lightSwitchApplication.ViewBetalinger().findContentItem("columns"); }],
        /// <field>
        /// Called after the left content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        left_postRender: [$element, function () { return new lightSwitchApplication.ViewBetalinger().findContentItem("left"); }],
        /// <field>
        /// Called after the Id content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        Id_postRender: [$element, function () { return new lightSwitchApplication.ViewBetalinger().findContentItem("Id"); }],
        /// <field>
        /// Called after the tblProjekt content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        tblProjekt_postRender: [$element, function () { return new lightSwitchApplication.ViewBetalinger().findContentItem("tblProjekt"); }],
        /// <field>
        /// Called after the tblMedlem_Nr content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        tblMedlem_Nr_postRender: [$element, function () { return new lightSwitchApplication.ViewBetalinger().findContentItem("tblMedlem_Nr"); }],
        /// <field>
        /// Called after the tblMedlem content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        tblMedlem_postRender: [$element, function () { return new lightSwitchApplication.ViewBetalinger().findContentItem("tblMedlem"); }],
        /// <field>
        /// Called after the Belob content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        Belob_postRender: [$element, function () { return new lightSwitchApplication.ViewBetalinger().findContentItem("Belob"); }],
        /// <field>
        /// Called after the BetalingsDato content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        BetalingsDato_postRender: [$element, function () { return new lightSwitchApplication.ViewBetalinger().findContentItem("BetalingsDato"); }],
        /// <field>
        /// Called after the FIKnr content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        FIKnr_postRender: [$element, function () { return new lightSwitchApplication.ViewBetalinger().findContentItem("FIKnr"); }],
        /// <field>
        /// Called after the right content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        right_postRender: [$element, function () { return new lightSwitchApplication.ViewBetalinger().findContentItem("right"); }]
    });

    lightSwitchApplication.AddEditProjekt.prototype._$contentItems = {
        Tabs: {
            _$class: msls.ContentItem,
            _$name: "Tabs",
            _$parentName: "RootContentItem",
            screen: lightSwitchApplication.AddEditProjekt
        },
        Details: {
            _$class: msls.ContentItem,
            _$name: "Details",
            _$parentName: "Tabs",
            screen: lightSwitchApplication.AddEditProjekt,
            data: lightSwitchApplication.AddEditProjekt,
            value: lightSwitchApplication.AddEditProjekt
        },
        columns: {
            _$class: msls.ContentItem,
            _$name: "columns",
            _$parentName: "Details",
            screen: lightSwitchApplication.AddEditProjekt,
            data: lightSwitchApplication.AddEditProjekt,
            value: lightSwitchApplication.tblProjekt
        },
        left: {
            _$class: msls.ContentItem,
            _$name: "left",
            _$parentName: "columns",
            screen: lightSwitchApplication.AddEditProjekt,
            data: lightSwitchApplication.tblProjekt,
            value: lightSwitchApplication.tblProjekt
        },
        Projektnr: {
            _$class: msls.ContentItem,
            _$name: "Projektnr",
            _$parentName: "left",
            screen: lightSwitchApplication.AddEditProjekt,
            data: lightSwitchApplication.tblProjekt,
            value: Number
        },
        Navn: {
            _$class: msls.ContentItem,
            _$name: "Navn",
            _$parentName: "left",
            screen: lightSwitchApplication.AddEditProjekt,
            data: lightSwitchApplication.tblProjekt,
            value: String
        },
        right: {
            _$class: msls.ContentItem,
            _$name: "right",
            _$parentName: "columns",
            screen: lightSwitchApplication.AddEditProjekt,
            data: lightSwitchApplication.tblProjekt,
            value: lightSwitchApplication.tblProjekt
        },
        Popups: {
            _$class: msls.ContentItem,
            _$name: "Popups",
            _$parentName: "RootContentItem",
            screen: lightSwitchApplication.AddEditProjekt
        }
    };

    msls._addEntryPoints(lightSwitchApplication.AddEditProjekt, {
        /// <field>
        /// Called when a new AddEditProjekt screen is created.
        /// <br/>created(msls.application.AddEditProjekt screen)
        /// </field>
        created: [lightSwitchApplication.AddEditProjekt],
        /// <field>
        /// Called before changes on an active AddEditProjekt screen are applied.
        /// <br/>beforeApplyChanges(msls.application.AddEditProjekt screen)
        /// </field>
        beforeApplyChanges: [lightSwitchApplication.AddEditProjekt],
        /// <field>
        /// Called after the Details content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        Details_postRender: [$element, function () { return new lightSwitchApplication.AddEditProjekt().findContentItem("Details"); }],
        /// <field>
        /// Called after the columns content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        columns_postRender: [$element, function () { return new lightSwitchApplication.AddEditProjekt().findContentItem("columns"); }],
        /// <field>
        /// Called after the left content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        left_postRender: [$element, function () { return new lightSwitchApplication.AddEditProjekt().findContentItem("left"); }],
        /// <field>
        /// Called after the Projektnr content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        Projektnr_postRender: [$element, function () { return new lightSwitchApplication.AddEditProjekt().findContentItem("Projektnr"); }],
        /// <field>
        /// Called after the Navn content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        Navn_postRender: [$element, function () { return new lightSwitchApplication.AddEditProjekt().findContentItem("Navn"); }],
        /// <field>
        /// Called after the right content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        right_postRender: [$element, function () { return new lightSwitchApplication.AddEditProjekt().findContentItem("right"); }]
    });

    lightSwitchApplication.BrowseProjekts.prototype._$contentItems = {
        Tabs: {
            _$class: msls.ContentItem,
            _$name: "Tabs",
            _$parentName: "RootContentItem",
            screen: lightSwitchApplication.BrowseProjekts
        },
        ProjektList: {
            _$class: msls.ContentItem,
            _$name: "ProjektList",
            _$parentName: "Tabs",
            screen: lightSwitchApplication.BrowseProjekts,
            data: lightSwitchApplication.BrowseProjekts,
            value: lightSwitchApplication.BrowseProjekts
        },
        tblProjekts: {
            _$class: msls.ContentItem,
            _$name: "tblProjekts",
            _$parentName: "ProjektList",
            screen: lightSwitchApplication.BrowseProjekts,
            data: lightSwitchApplication.BrowseProjekts,
            value: {
                _$class: msls.VisualCollection,
                screen: lightSwitchApplication.BrowseProjekts,
                _$entry: {
                    elementType: lightSwitchApplication.tblProjekt
                }
            }
        },
        rows: {
            _$class: msls.ContentItem,
            _$name: "rows",
            _$parentName: "tblProjekts",
            screen: lightSwitchApplication.BrowseProjekts,
            data: lightSwitchApplication.tblProjekt,
            value: lightSwitchApplication.tblProjekt
        },
        Projektnr: {
            _$class: msls.ContentItem,
            _$name: "Projektnr",
            _$parentName: "rows",
            screen: lightSwitchApplication.BrowseProjekts,
            data: lightSwitchApplication.tblProjekt,
            value: Number
        },
        Navn: {
            _$class: msls.ContentItem,
            _$name: "Navn",
            _$parentName: "rows",
            screen: lightSwitchApplication.BrowseProjekts,
            data: lightSwitchApplication.tblProjekt,
            value: String
        },
        Popups: {
            _$class: msls.ContentItem,
            _$name: "Popups",
            _$parentName: "RootContentItem",
            screen: lightSwitchApplication.BrowseProjekts
        }
    };

    msls._addEntryPoints(lightSwitchApplication.BrowseProjekts, {
        /// <field>
        /// Called when a new BrowseProjekts screen is created.
        /// <br/>created(msls.application.BrowseProjekts screen)
        /// </field>
        created: [lightSwitchApplication.BrowseProjekts],
        /// <field>
        /// Called before changes on an active BrowseProjekts screen are applied.
        /// <br/>beforeApplyChanges(msls.application.BrowseProjekts screen)
        /// </field>
        beforeApplyChanges: [lightSwitchApplication.BrowseProjekts],
        /// <field>
        /// Called after the ProjektList content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        ProjektList_postRender: [$element, function () { return new lightSwitchApplication.BrowseProjekts().findContentItem("ProjektList"); }],
        /// <field>
        /// Called after the tblProjekts content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        tblProjekts_postRender: [$element, function () { return new lightSwitchApplication.BrowseProjekts().findContentItem("tblProjekts"); }],
        /// <field>
        /// Called after the rows content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        rows_postRender: [$element, function () { return new lightSwitchApplication.BrowseProjekts().findContentItem("rows"); }],
        /// <field>
        /// Called after the Projektnr content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        Projektnr_postRender: [$element, function () { return new lightSwitchApplication.BrowseProjekts().findContentItem("Projektnr"); }],
        /// <field>
        /// Called after the Navn content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        Navn_postRender: [$element, function () { return new lightSwitchApplication.BrowseProjekts().findContentItem("Navn"); }]
    });

    lightSwitchApplication.ViewProjekt.prototype._$contentItems = {
        Tabs: {
            _$class: msls.ContentItem,
            _$name: "Tabs",
            _$parentName: "RootContentItem",
            screen: lightSwitchApplication.ViewProjekt
        },
        Details: {
            _$class: msls.ContentItem,
            _$name: "Details",
            _$parentName: "Tabs",
            screen: lightSwitchApplication.ViewProjekt,
            data: lightSwitchApplication.ViewProjekt,
            value: lightSwitchApplication.ViewProjekt
        },
        columns: {
            _$class: msls.ContentItem,
            _$name: "columns",
            _$parentName: "Details",
            screen: lightSwitchApplication.ViewProjekt,
            data: lightSwitchApplication.ViewProjekt,
            value: lightSwitchApplication.tblProjekt
        },
        left: {
            _$class: msls.ContentItem,
            _$name: "left",
            _$parentName: "columns",
            screen: lightSwitchApplication.ViewProjekt,
            data: lightSwitchApplication.tblProjekt,
            value: lightSwitchApplication.tblProjekt
        },
        Projektnr: {
            _$class: msls.ContentItem,
            _$name: "Projektnr",
            _$parentName: "left",
            screen: lightSwitchApplication.ViewProjekt,
            data: lightSwitchApplication.tblProjekt,
            value: Number
        },
        Navn: {
            _$class: msls.ContentItem,
            _$name: "Navn",
            _$parentName: "left",
            screen: lightSwitchApplication.ViewProjekt,
            data: lightSwitchApplication.tblProjekt,
            value: String
        },
        right: {
            _$class: msls.ContentItem,
            _$name: "right",
            _$parentName: "columns",
            screen: lightSwitchApplication.ViewProjekt,
            data: lightSwitchApplication.tblProjekt,
            value: lightSwitchApplication.tblProjekt
        },
        Popups: {
            _$class: msls.ContentItem,
            _$name: "Popups",
            _$parentName: "RootContentItem",
            screen: lightSwitchApplication.ViewProjekt
        }
    };

    msls._addEntryPoints(lightSwitchApplication.ViewProjekt, {
        /// <field>
        /// Called when a new ViewProjekt screen is created.
        /// <br/>created(msls.application.ViewProjekt screen)
        /// </field>
        created: [lightSwitchApplication.ViewProjekt],
        /// <field>
        /// Called before changes on an active ViewProjekt screen are applied.
        /// <br/>beforeApplyChanges(msls.application.ViewProjekt screen)
        /// </field>
        beforeApplyChanges: [lightSwitchApplication.ViewProjekt],
        /// <field>
        /// Called after the Details content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        Details_postRender: [$element, function () { return new lightSwitchApplication.ViewProjekt().findContentItem("Details"); }],
        /// <field>
        /// Called after the columns content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        columns_postRender: [$element, function () { return new lightSwitchApplication.ViewProjekt().findContentItem("columns"); }],
        /// <field>
        /// Called after the left content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        left_postRender: [$element, function () { return new lightSwitchApplication.ViewProjekt().findContentItem("left"); }],
        /// <field>
        /// Called after the Projektnr content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        Projektnr_postRender: [$element, function () { return new lightSwitchApplication.ViewProjekt().findContentItem("Projektnr"); }],
        /// <field>
        /// Called after the Navn content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        Navn_postRender: [$element, function () { return new lightSwitchApplication.ViewProjekt().findContentItem("Navn"); }],
        /// <field>
        /// Called after the right content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        right_postRender: [$element, function () { return new lightSwitchApplication.ViewProjekt().findContentItem("right"); }]
    });

    lightSwitchApplication.Medlemmer.prototype._$contentItems = {
        Tabs: {
            _$class: msls.ContentItem,
            _$name: "Tabs",
            _$parentName: "RootContentItem",
            screen: lightSwitchApplication.Medlemmer
        },
        tblMedlemList: {
            _$class: msls.ContentItem,
            _$name: "tblMedlemList",
            _$parentName: "Tabs",
            screen: lightSwitchApplication.Medlemmer,
            data: lightSwitchApplication.Medlemmer,
            value: lightSwitchApplication.Medlemmer
        },
        tblMedlems: {
            _$class: msls.ContentItem,
            _$name: "tblMedlems",
            _$parentName: "tblMedlemList",
            screen: lightSwitchApplication.Medlemmer,
            data: lightSwitchApplication.Medlemmer,
            value: {
                _$class: msls.VisualCollection,
                screen: lightSwitchApplication.Medlemmer,
                _$entry: {
                    elementType: lightSwitchApplication.tblMedlem
                }
            }
        },
        rows: {
            _$class: msls.ContentItem,
            _$name: "rows",
            _$parentName: "tblMedlems",
            screen: lightSwitchApplication.Medlemmer,
            data: lightSwitchApplication.tblMedlem,
            value: lightSwitchApplication.tblMedlem
        },
        Navn: {
            _$class: msls.ContentItem,
            _$name: "Navn",
            _$parentName: "rows",
            screen: lightSwitchApplication.Medlemmer,
            data: lightSwitchApplication.tblMedlem,
            value: String
        },
        Nr: {
            _$class: msls.ContentItem,
            _$name: "Nr",
            _$parentName: "rows",
            screen: lightSwitchApplication.Medlemmer,
            data: lightSwitchApplication.tblMedlem,
            value: Number
        },
        Adresse: {
            _$class: msls.ContentItem,
            _$name: "Adresse",
            _$parentName: "rows",
            screen: lightSwitchApplication.Medlemmer,
            data: lightSwitchApplication.tblMedlem,
            value: String
        },
        Bynavn: {
            _$class: msls.ContentItem,
            _$name: "Bynavn",
            _$parentName: "rows",
            screen: lightSwitchApplication.Medlemmer,
            data: lightSwitchApplication.tblMedlem,
            value: String
        },
        Telefon: {
            _$class: msls.ContentItem,
            _$name: "Telefon",
            _$parentName: "rows",
            screen: lightSwitchApplication.Medlemmer,
            data: lightSwitchApplication.tblMedlem,
            value: String
        },
        Email: {
            _$class: msls.ContentItem,
            _$name: "Email",
            _$parentName: "rows",
            screen: lightSwitchApplication.Medlemmer,
            data: lightSwitchApplication.tblMedlem,
            value: String
        },
        Popups: {
            _$class: msls.ContentItem,
            _$name: "Popups",
            _$parentName: "RootContentItem",
            screen: lightSwitchApplication.Medlemmer
        }
    };

    msls._addEntryPoints(lightSwitchApplication.Medlemmer, {
        /// <field>
        /// Called when a new Medlemmer screen is created.
        /// <br/>created(msls.application.Medlemmer screen)
        /// </field>
        created: [lightSwitchApplication.Medlemmer],
        /// <field>
        /// Called before changes on an active Medlemmer screen are applied.
        /// <br/>beforeApplyChanges(msls.application.Medlemmer screen)
        /// </field>
        beforeApplyChanges: [lightSwitchApplication.Medlemmer],
        /// <field>
        /// Called after the tblMedlemList content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        tblMedlemList_postRender: [$element, function () { return new lightSwitchApplication.Medlemmer().findContentItem("tblMedlemList"); }],
        /// <field>
        /// Called after the tblMedlems content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        tblMedlems_postRender: [$element, function () { return new lightSwitchApplication.Medlemmer().findContentItem("tblMedlems"); }],
        /// <field>
        /// Called after the rows content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        rows_postRender: [$element, function () { return new lightSwitchApplication.Medlemmer().findContentItem("rows"); }],
        /// <field>
        /// Called after the Navn content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        Navn_postRender: [$element, function () { return new lightSwitchApplication.Medlemmer().findContentItem("Navn"); }],
        /// <field>
        /// Called after the Nr content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        Nr_postRender: [$element, function () { return new lightSwitchApplication.Medlemmer().findContentItem("Nr"); }],
        /// <field>
        /// Called after the Adresse content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        Adresse_postRender: [$element, function () { return new lightSwitchApplication.Medlemmer().findContentItem("Adresse"); }],
        /// <field>
        /// Called after the Bynavn content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        Bynavn_postRender: [$element, function () { return new lightSwitchApplication.Medlemmer().findContentItem("Bynavn"); }],
        /// <field>
        /// Called after the Telefon content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        Telefon_postRender: [$element, function () { return new lightSwitchApplication.Medlemmer().findContentItem("Telefon"); }],
        /// <field>
        /// Called after the Email content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        Email_postRender: [$element, function () { return new lightSwitchApplication.Medlemmer().findContentItem("Email"); }]
    });

}(msls.application));