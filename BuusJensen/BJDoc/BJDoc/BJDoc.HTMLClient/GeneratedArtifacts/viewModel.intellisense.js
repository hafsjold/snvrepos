/// <reference path="viewModel.js" />

(function (lightSwitchApplication) {

    var $element = document.createElement("div");

    lightSwitchApplication.AddEditBruger.prototype._$contentItems = {
        Tabs: {
            _$class: msls.ContentItem,
            _$name: "Tabs",
            _$parentName: "RootContentItem",
            screen: lightSwitchApplication.AddEditBruger
        },
        Details: {
            _$class: msls.ContentItem,
            _$name: "Details",
            _$parentName: "Tabs",
            screen: lightSwitchApplication.AddEditBruger,
            data: lightSwitchApplication.AddEditBruger,
            value: lightSwitchApplication.AddEditBruger
        },
        columns: {
            _$class: msls.ContentItem,
            _$name: "columns",
            _$parentName: "Details",
            screen: lightSwitchApplication.AddEditBruger,
            data: lightSwitchApplication.AddEditBruger,
            value: lightSwitchApplication.tblBruger
        },
        left: {
            _$class: msls.ContentItem,
            _$name: "left",
            _$parentName: "columns",
            screen: lightSwitchApplication.AddEditBruger,
            data: lightSwitchApplication.tblBruger,
            value: lightSwitchApplication.tblBruger
        },
        initialer: {
            _$class: msls.ContentItem,
            _$name: "initialer",
            _$parentName: "left",
            screen: lightSwitchApplication.AddEditBruger,
            data: lightSwitchApplication.tblBruger,
            value: String
        },
        navn: {
            _$class: msls.ContentItem,
            _$name: "navn",
            _$parentName: "left",
            screen: lightSwitchApplication.AddEditBruger,
            data: lightSwitchApplication.tblBruger,
            value: String
        },
        telefon: {
            _$class: msls.ContentItem,
            _$name: "telefon",
            _$parentName: "left",
            screen: lightSwitchApplication.AddEditBruger,
            data: lightSwitchApplication.tblBruger,
            value: String
        },
        mobil: {
            _$class: msls.ContentItem,
            _$name: "mobil",
            _$parentName: "left",
            screen: lightSwitchApplication.AddEditBruger,
            data: lightSwitchApplication.tblBruger,
            value: String
        },
        right: {
            _$class: msls.ContentItem,
            _$name: "right",
            _$parentName: "columns",
            screen: lightSwitchApplication.AddEditBruger,
            data: lightSwitchApplication.tblBruger,
            value: lightSwitchApplication.tblBruger
        },
        email: {
            _$class: msls.ContentItem,
            _$name: "email",
            _$parentName: "right",
            screen: lightSwitchApplication.AddEditBruger,
            data: lightSwitchApplication.tblBruger,
            value: String
        },
        statsaut: {
            _$class: msls.ContentItem,
            _$name: "statsaut",
            _$parentName: "right",
            screen: lightSwitchApplication.AddEditBruger,
            data: lightSwitchApplication.tblBruger,
            value: Boolean
        },
        partner: {
            _$class: msls.ContentItem,
            _$name: "partner",
            _$parentName: "right",
            screen: lightSwitchApplication.AddEditBruger,
            data: lightSwitchApplication.tblBruger,
            value: Boolean
        },
        Popups: {
            _$class: msls.ContentItem,
            _$name: "Popups",
            _$parentName: "RootContentItem",
            screen: lightSwitchApplication.AddEditBruger
        }
    };

    msls._addEntryPoints(lightSwitchApplication.AddEditBruger, {
        /// <field>
        /// Called when a new AddEditBruger screen is created.
        /// <br/>created(msls.application.AddEditBruger screen)
        /// </field>
        created: [lightSwitchApplication.AddEditBruger],
        /// <field>
        /// Called before changes on an active AddEditBruger screen are applied.
        /// <br/>beforeApplyChanges(msls.application.AddEditBruger screen)
        /// </field>
        beforeApplyChanges: [lightSwitchApplication.AddEditBruger],
        /// <field>
        /// Called after the Details content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        Details_postRender: [$element, function () { return new lightSwitchApplication.AddEditBruger().findContentItem("Details"); }],
        /// <field>
        /// Called after the columns content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        columns_postRender: [$element, function () { return new lightSwitchApplication.AddEditBruger().findContentItem("columns"); }],
        /// <field>
        /// Called after the left content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        left_postRender: [$element, function () { return new lightSwitchApplication.AddEditBruger().findContentItem("left"); }],
        /// <field>
        /// Called after the initialer content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        initialer_postRender: [$element, function () { return new lightSwitchApplication.AddEditBruger().findContentItem("initialer"); }],
        /// <field>
        /// Called after the navn content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        navn_postRender: [$element, function () { return new lightSwitchApplication.AddEditBruger().findContentItem("navn"); }],
        /// <field>
        /// Called after the telefon content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        telefon_postRender: [$element, function () { return new lightSwitchApplication.AddEditBruger().findContentItem("telefon"); }],
        /// <field>
        /// Called after the mobil content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        mobil_postRender: [$element, function () { return new lightSwitchApplication.AddEditBruger().findContentItem("mobil"); }],
        /// <field>
        /// Called after the right content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        right_postRender: [$element, function () { return new lightSwitchApplication.AddEditBruger().findContentItem("right"); }],
        /// <field>
        /// Called after the email content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        email_postRender: [$element, function () { return new lightSwitchApplication.AddEditBruger().findContentItem("email"); }],
        /// <field>
        /// Called after the statsaut content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        statsaut_postRender: [$element, function () { return new lightSwitchApplication.AddEditBruger().findContentItem("statsaut"); }],
        /// <field>
        /// Called after the partner content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        partner_postRender: [$element, function () { return new lightSwitchApplication.AddEditBruger().findContentItem("partner"); }]
    });

    lightSwitchApplication.BrowseBrugers.prototype._$contentItems = {
        Tabs: {
            _$class: msls.ContentItem,
            _$name: "Tabs",
            _$parentName: "RootContentItem",
            screen: lightSwitchApplication.BrowseBrugers
        },
        BrugerList: {
            _$class: msls.ContentItem,
            _$name: "BrugerList",
            _$parentName: "Tabs",
            screen: lightSwitchApplication.BrowseBrugers,
            data: lightSwitchApplication.BrowseBrugers,
            value: lightSwitchApplication.BrowseBrugers
        },
        tblBrugers: {
            _$class: msls.ContentItem,
            _$name: "tblBrugers",
            _$parentName: "BrugerList",
            screen: lightSwitchApplication.BrowseBrugers,
            data: lightSwitchApplication.BrowseBrugers,
            value: {
                _$class: msls.VisualCollection,
                screen: lightSwitchApplication.BrowseBrugers,
                _$entry: {
                    elementType: lightSwitchApplication.tblBruger
                }
            }
        },
        rows: {
            _$class: msls.ContentItem,
            _$name: "rows",
            _$parentName: "tblBrugers",
            screen: lightSwitchApplication.BrowseBrugers,
            data: lightSwitchApplication.tblBruger,
            value: lightSwitchApplication.tblBruger
        },
        initialer: {
            _$class: msls.ContentItem,
            _$name: "initialer",
            _$parentName: "rows",
            screen: lightSwitchApplication.BrowseBrugers,
            data: lightSwitchApplication.tblBruger,
            value: String
        },
        navn: {
            _$class: msls.ContentItem,
            _$name: "navn",
            _$parentName: "rows",
            screen: lightSwitchApplication.BrowseBrugers,
            data: lightSwitchApplication.tblBruger,
            value: String
        },
        telefon: {
            _$class: msls.ContentItem,
            _$name: "telefon",
            _$parentName: "rows",
            screen: lightSwitchApplication.BrowseBrugers,
            data: lightSwitchApplication.tblBruger,
            value: String
        },
        Popups: {
            _$class: msls.ContentItem,
            _$name: "Popups",
            _$parentName: "RootContentItem",
            screen: lightSwitchApplication.BrowseBrugers
        }
    };

    msls._addEntryPoints(lightSwitchApplication.BrowseBrugers, {
        /// <field>
        /// Called when a new BrowseBrugers screen is created.
        /// <br/>created(msls.application.BrowseBrugers screen)
        /// </field>
        created: [lightSwitchApplication.BrowseBrugers],
        /// <field>
        /// Called before changes on an active BrowseBrugers screen are applied.
        /// <br/>beforeApplyChanges(msls.application.BrowseBrugers screen)
        /// </field>
        beforeApplyChanges: [lightSwitchApplication.BrowseBrugers],
        /// <field>
        /// Called after the BrugerList content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        BrugerList_postRender: [$element, function () { return new lightSwitchApplication.BrowseBrugers().findContentItem("BrugerList"); }],
        /// <field>
        /// Called after the tblBrugers content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        tblBrugers_postRender: [$element, function () { return new lightSwitchApplication.BrowseBrugers().findContentItem("tblBrugers"); }],
        /// <field>
        /// Called after the rows content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        rows_postRender: [$element, function () { return new lightSwitchApplication.BrowseBrugers().findContentItem("rows"); }],
        /// <field>
        /// Called after the initialer content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        initialer_postRender: [$element, function () { return new lightSwitchApplication.BrowseBrugers().findContentItem("initialer"); }],
        /// <field>
        /// Called after the navn content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        navn_postRender: [$element, function () { return new lightSwitchApplication.BrowseBrugers().findContentItem("navn"); }],
        /// <field>
        /// Called after the telefon content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        telefon_postRender: [$element, function () { return new lightSwitchApplication.BrowseBrugers().findContentItem("telefon"); }]
    });

    lightSwitchApplication.ViewBruger.prototype._$contentItems = {
        Tabs: {
            _$class: msls.ContentItem,
            _$name: "Tabs",
            _$parentName: "RootContentItem",
            screen: lightSwitchApplication.ViewBruger
        },
        Details: {
            _$class: msls.ContentItem,
            _$name: "Details",
            _$parentName: "Tabs",
            screen: lightSwitchApplication.ViewBruger,
            data: lightSwitchApplication.ViewBruger,
            value: lightSwitchApplication.ViewBruger
        },
        columns: {
            _$class: msls.ContentItem,
            _$name: "columns",
            _$parentName: "Details",
            screen: lightSwitchApplication.ViewBruger,
            data: lightSwitchApplication.ViewBruger,
            value: lightSwitchApplication.tblBruger
        },
        left: {
            _$class: msls.ContentItem,
            _$name: "left",
            _$parentName: "columns",
            screen: lightSwitchApplication.ViewBruger,
            data: lightSwitchApplication.tblBruger,
            value: lightSwitchApplication.tblBruger
        },
        initialer: {
            _$class: msls.ContentItem,
            _$name: "initialer",
            _$parentName: "left",
            screen: lightSwitchApplication.ViewBruger,
            data: lightSwitchApplication.tblBruger,
            value: String
        },
        navn: {
            _$class: msls.ContentItem,
            _$name: "navn",
            _$parentName: "left",
            screen: lightSwitchApplication.ViewBruger,
            data: lightSwitchApplication.tblBruger,
            value: String
        },
        telefon: {
            _$class: msls.ContentItem,
            _$name: "telefon",
            _$parentName: "left",
            screen: lightSwitchApplication.ViewBruger,
            data: lightSwitchApplication.tblBruger,
            value: String
        },
        mobil: {
            _$class: msls.ContentItem,
            _$name: "mobil",
            _$parentName: "left",
            screen: lightSwitchApplication.ViewBruger,
            data: lightSwitchApplication.tblBruger,
            value: String
        },
        email: {
            _$class: msls.ContentItem,
            _$name: "email",
            _$parentName: "left",
            screen: lightSwitchApplication.ViewBruger,
            data: lightSwitchApplication.tblBruger,
            value: String
        },
        statsaut: {
            _$class: msls.ContentItem,
            _$name: "statsaut",
            _$parentName: "left",
            screen: lightSwitchApplication.ViewBruger,
            data: lightSwitchApplication.tblBruger,
            value: Boolean
        },
        right: {
            _$class: msls.ContentItem,
            _$name: "right",
            _$parentName: "columns",
            screen: lightSwitchApplication.ViewBruger,
            data: lightSwitchApplication.tblBruger,
            value: lightSwitchApplication.tblBruger
        },
        partner: {
            _$class: msls.ContentItem,
            _$name: "partner",
            _$parentName: "right",
            screen: lightSwitchApplication.ViewBruger,
            data: lightSwitchApplication.tblBruger,
            value: Boolean
        },
        CreatedBy: {
            _$class: msls.ContentItem,
            _$name: "CreatedBy",
            _$parentName: "right",
            screen: lightSwitchApplication.ViewBruger,
            data: lightSwitchApplication.tblBruger,
            value: String
        },
        Created: {
            _$class: msls.ContentItem,
            _$name: "Created",
            _$parentName: "right",
            screen: lightSwitchApplication.ViewBruger,
            data: lightSwitchApplication.tblBruger,
            value: Date
        },
        ModifiedBy: {
            _$class: msls.ContentItem,
            _$name: "ModifiedBy",
            _$parentName: "right",
            screen: lightSwitchApplication.ViewBruger,
            data: lightSwitchApplication.tblBruger,
            value: String
        },
        Modified: {
            _$class: msls.ContentItem,
            _$name: "Modified",
            _$parentName: "right",
            screen: lightSwitchApplication.ViewBruger,
            data: lightSwitchApplication.tblBruger,
            value: Date
        },
        Popups: {
            _$class: msls.ContentItem,
            _$name: "Popups",
            _$parentName: "RootContentItem",
            screen: lightSwitchApplication.ViewBruger
        }
    };

    msls._addEntryPoints(lightSwitchApplication.ViewBruger, {
        /// <field>
        /// Called when a new ViewBruger screen is created.
        /// <br/>created(msls.application.ViewBruger screen)
        /// </field>
        created: [lightSwitchApplication.ViewBruger],
        /// <field>
        /// Called before changes on an active ViewBruger screen are applied.
        /// <br/>beforeApplyChanges(msls.application.ViewBruger screen)
        /// </field>
        beforeApplyChanges: [lightSwitchApplication.ViewBruger],
        /// <field>
        /// Called after the Details content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        Details_postRender: [$element, function () { return new lightSwitchApplication.ViewBruger().findContentItem("Details"); }],
        /// <field>
        /// Called after the columns content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        columns_postRender: [$element, function () { return new lightSwitchApplication.ViewBruger().findContentItem("columns"); }],
        /// <field>
        /// Called after the left content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        left_postRender: [$element, function () { return new lightSwitchApplication.ViewBruger().findContentItem("left"); }],
        /// <field>
        /// Called after the initialer content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        initialer_postRender: [$element, function () { return new lightSwitchApplication.ViewBruger().findContentItem("initialer"); }],
        /// <field>
        /// Called after the navn content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        navn_postRender: [$element, function () { return new lightSwitchApplication.ViewBruger().findContentItem("navn"); }],
        /// <field>
        /// Called after the telefon content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        telefon_postRender: [$element, function () { return new lightSwitchApplication.ViewBruger().findContentItem("telefon"); }],
        /// <field>
        /// Called after the mobil content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        mobil_postRender: [$element, function () { return new lightSwitchApplication.ViewBruger().findContentItem("mobil"); }],
        /// <field>
        /// Called after the email content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        email_postRender: [$element, function () { return new lightSwitchApplication.ViewBruger().findContentItem("email"); }],
        /// <field>
        /// Called after the statsaut content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        statsaut_postRender: [$element, function () { return new lightSwitchApplication.ViewBruger().findContentItem("statsaut"); }],
        /// <field>
        /// Called after the right content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        right_postRender: [$element, function () { return new lightSwitchApplication.ViewBruger().findContentItem("right"); }],
        /// <field>
        /// Called after the partner content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        partner_postRender: [$element, function () { return new lightSwitchApplication.ViewBruger().findContentItem("partner"); }],
        /// <field>
        /// Called after the CreatedBy content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        CreatedBy_postRender: [$element, function () { return new lightSwitchApplication.ViewBruger().findContentItem("CreatedBy"); }],
        /// <field>
        /// Called after the Created content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        Created_postRender: [$element, function () { return new lightSwitchApplication.ViewBruger().findContentItem("Created"); }],
        /// <field>
        /// Called after the ModifiedBy content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        ModifiedBy_postRender: [$element, function () { return new lightSwitchApplication.ViewBruger().findContentItem("ModifiedBy"); }],
        /// <field>
        /// Called after the Modified content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        Modified_postRender: [$element, function () { return new lightSwitchApplication.ViewBruger().findContentItem("Modified"); }]
    });

    lightSwitchApplication.AddEditComputer.prototype._$contentItems = {
        Tabs: {
            _$class: msls.ContentItem,
            _$name: "Tabs",
            _$parentName: "RootContentItem",
            screen: lightSwitchApplication.AddEditComputer
        },
        Details: {
            _$class: msls.ContentItem,
            _$name: "Details",
            _$parentName: "Tabs",
            screen: lightSwitchApplication.AddEditComputer,
            data: lightSwitchApplication.AddEditComputer,
            value: lightSwitchApplication.AddEditComputer
        },
        columns: {
            _$class: msls.ContentItem,
            _$name: "columns",
            _$parentName: "Details",
            screen: lightSwitchApplication.AddEditComputer,
            data: lightSwitchApplication.AddEditComputer,
            value: lightSwitchApplication.tblComputer
        },
        left: {
            _$class: msls.ContentItem,
            _$name: "left",
            _$parentName: "columns",
            screen: lightSwitchApplication.AddEditComputer,
            data: lightSwitchApplication.tblComputer,
            value: lightSwitchApplication.tblComputer
        },
        serienr: {
            _$class: msls.ContentItem,
            _$name: "serienr",
            _$parentName: "left",
            screen: lightSwitchApplication.AddEditComputer,
            data: lightSwitchApplication.tblComputer,
            value: String
        },
        tblHW: {
            _$class: msls.ContentItem,
            _$name: "tblHW",
            _$parentName: "left",
            screen: lightSwitchApplication.AddEditComputer,
            data: lightSwitchApplication.tblComputer,
            value: lightSwitchApplication.tblHW
        },
        RowTemplate: {
            _$class: msls.ContentItem,
            _$name: "RowTemplate",
            _$parentName: "tblHW",
            screen: lightSwitchApplication.AddEditComputer,
            data: lightSwitchApplication.tblHW,
            value: lightSwitchApplication.tblHW
        },
        tblBruger: {
            _$class: msls.ContentItem,
            _$name: "tblBruger",
            _$parentName: "left",
            screen: lightSwitchApplication.AddEditComputer,
            data: lightSwitchApplication.tblComputer,
            value: lightSwitchApplication.tblBruger
        },
        RowTemplate1: {
            _$class: msls.ContentItem,
            _$name: "RowTemplate1",
            _$parentName: "tblBruger",
            screen: lightSwitchApplication.AddEditComputer,
            data: lightSwitchApplication.tblBruger,
            value: lightSwitchApplication.tblBruger
        },
        note: {
            _$class: msls.ContentItem,
            _$name: "note",
            _$parentName: "left",
            screen: lightSwitchApplication.AddEditComputer,
            data: lightSwitchApplication.tblComputer,
            value: String
        },
        right: {
            _$class: msls.ContentItem,
            _$name: "right",
            _$parentName: "columns",
            screen: lightSwitchApplication.AddEditComputer,
            data: lightSwitchApplication.tblComputer,
            value: lightSwitchApplication.tblComputer
        },
        Popups: {
            _$class: msls.ContentItem,
            _$name: "Popups",
            _$parentName: "RootContentItem",
            screen: lightSwitchApplication.AddEditComputer
        }
    };

    msls._addEntryPoints(lightSwitchApplication.AddEditComputer, {
        /// <field>
        /// Called when a new AddEditComputer screen is created.
        /// <br/>created(msls.application.AddEditComputer screen)
        /// </field>
        created: [lightSwitchApplication.AddEditComputer],
        /// <field>
        /// Called before changes on an active AddEditComputer screen are applied.
        /// <br/>beforeApplyChanges(msls.application.AddEditComputer screen)
        /// </field>
        beforeApplyChanges: [lightSwitchApplication.AddEditComputer],
        /// <field>
        /// Called after the Details content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        Details_postRender: [$element, function () { return new lightSwitchApplication.AddEditComputer().findContentItem("Details"); }],
        /// <field>
        /// Called after the columns content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        columns_postRender: [$element, function () { return new lightSwitchApplication.AddEditComputer().findContentItem("columns"); }],
        /// <field>
        /// Called after the left content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        left_postRender: [$element, function () { return new lightSwitchApplication.AddEditComputer().findContentItem("left"); }],
        /// <field>
        /// Called after the serienr content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        serienr_postRender: [$element, function () { return new lightSwitchApplication.AddEditComputer().findContentItem("serienr"); }],
        /// <field>
        /// Called after the tblHW content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        tblHW_postRender: [$element, function () { return new lightSwitchApplication.AddEditComputer().findContentItem("tblHW"); }],
        /// <field>
        /// Called after the RowTemplate content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        RowTemplate_postRender: [$element, function () { return new lightSwitchApplication.AddEditComputer().findContentItem("RowTemplate"); }],
        /// <field>
        /// Called after the tblBruger content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        tblBruger_postRender: [$element, function () { return new lightSwitchApplication.AddEditComputer().findContentItem("tblBruger"); }],
        /// <field>
        /// Called after the RowTemplate1 content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        RowTemplate1_postRender: [$element, function () { return new lightSwitchApplication.AddEditComputer().findContentItem("RowTemplate1"); }],
        /// <field>
        /// Called after the note content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        note_postRender: [$element, function () { return new lightSwitchApplication.AddEditComputer().findContentItem("note"); }],
        /// <field>
        /// Called after the right content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        right_postRender: [$element, function () { return new lightSwitchApplication.AddEditComputer().findContentItem("right"); }]
    });

    lightSwitchApplication.BrowseComputers.prototype._$contentItems = {
        Tabs: {
            _$class: msls.ContentItem,
            _$name: "Tabs",
            _$parentName: "RootContentItem",
            screen: lightSwitchApplication.BrowseComputers
        },
        ComputerList: {
            _$class: msls.ContentItem,
            _$name: "ComputerList",
            _$parentName: "Tabs",
            screen: lightSwitchApplication.BrowseComputers,
            data: lightSwitchApplication.BrowseComputers,
            value: lightSwitchApplication.BrowseComputers
        },
        tblComputers: {
            _$class: msls.ContentItem,
            _$name: "tblComputers",
            _$parentName: "ComputerList",
            screen: lightSwitchApplication.BrowseComputers,
            data: lightSwitchApplication.BrowseComputers,
            value: {
                _$class: msls.VisualCollection,
                screen: lightSwitchApplication.BrowseComputers,
                _$entry: {
                    elementType: lightSwitchApplication.tblComputer
                }
            }
        },
        rows: {
            _$class: msls.ContentItem,
            _$name: "rows",
            _$parentName: "tblComputers",
            screen: lightSwitchApplication.BrowseComputers,
            data: lightSwitchApplication.tblComputer,
            value: lightSwitchApplication.tblComputer
        },
        Id: {
            _$class: msls.ContentItem,
            _$name: "Id",
            _$parentName: "rows",
            screen: lightSwitchApplication.BrowseComputers,
            data: lightSwitchApplication.tblComputer,
            value: Number
        },
        serienr: {
            _$class: msls.ContentItem,
            _$name: "serienr",
            _$parentName: "rows",
            screen: lightSwitchApplication.BrowseComputers,
            data: lightSwitchApplication.tblComputer,
            value: String
        },
        tblHW: {
            _$class: msls.ContentItem,
            _$name: "tblHW",
            _$parentName: "rows",
            screen: lightSwitchApplication.BrowseComputers,
            data: lightSwitchApplication.tblComputer,
            value: lightSwitchApplication.tblHW
        },
        tblBruger: {
            _$class: msls.ContentItem,
            _$name: "tblBruger",
            _$parentName: "rows",
            screen: lightSwitchApplication.BrowseComputers,
            data: lightSwitchApplication.tblComputer,
            value: lightSwitchApplication.tblBruger
        },
        note: {
            _$class: msls.ContentItem,
            _$name: "note",
            _$parentName: "rows",
            screen: lightSwitchApplication.BrowseComputers,
            data: lightSwitchApplication.tblComputer,
            value: String
        },
        Popups: {
            _$class: msls.ContentItem,
            _$name: "Popups",
            _$parentName: "RootContentItem",
            screen: lightSwitchApplication.BrowseComputers
        }
    };

    msls._addEntryPoints(lightSwitchApplication.BrowseComputers, {
        /// <field>
        /// Called when a new BrowseComputers screen is created.
        /// <br/>created(msls.application.BrowseComputers screen)
        /// </field>
        created: [lightSwitchApplication.BrowseComputers],
        /// <field>
        /// Called before changes on an active BrowseComputers screen are applied.
        /// <br/>beforeApplyChanges(msls.application.BrowseComputers screen)
        /// </field>
        beforeApplyChanges: [lightSwitchApplication.BrowseComputers],
        /// <field>
        /// Called after the ComputerList content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        ComputerList_postRender: [$element, function () { return new lightSwitchApplication.BrowseComputers().findContentItem("ComputerList"); }],
        /// <field>
        /// Called after the tblComputers content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        tblComputers_postRender: [$element, function () { return new lightSwitchApplication.BrowseComputers().findContentItem("tblComputers"); }],
        /// <field>
        /// Called after the rows content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        rows_postRender: [$element, function () { return new lightSwitchApplication.BrowseComputers().findContentItem("rows"); }],
        /// <field>
        /// Called after the Id content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        Id_postRender: [$element, function () { return new lightSwitchApplication.BrowseComputers().findContentItem("Id"); }],
        /// <field>
        /// Called after the serienr content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        serienr_postRender: [$element, function () { return new lightSwitchApplication.BrowseComputers().findContentItem("serienr"); }],
        /// <field>
        /// Called after the tblHW content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        tblHW_postRender: [$element, function () { return new lightSwitchApplication.BrowseComputers().findContentItem("tblHW"); }],
        /// <field>
        /// Called after the tblBruger content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        tblBruger_postRender: [$element, function () { return new lightSwitchApplication.BrowseComputers().findContentItem("tblBruger"); }],
        /// <field>
        /// Called after the note content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        note_postRender: [$element, function () { return new lightSwitchApplication.BrowseComputers().findContentItem("note"); }]
    });

    lightSwitchApplication.ViewComputer.prototype._$contentItems = {
        Tabs: {
            _$class: msls.ContentItem,
            _$name: "Tabs",
            _$parentName: "RootContentItem",
            screen: lightSwitchApplication.ViewComputer
        },
        Details: {
            _$class: msls.ContentItem,
            _$name: "Details",
            _$parentName: "Tabs",
            screen: lightSwitchApplication.ViewComputer,
            data: lightSwitchApplication.ViewComputer,
            value: lightSwitchApplication.ViewComputer
        },
        columns: {
            _$class: msls.ContentItem,
            _$name: "columns",
            _$parentName: "Details",
            screen: lightSwitchApplication.ViewComputer,
            data: lightSwitchApplication.ViewComputer,
            value: lightSwitchApplication.tblComputer
        },
        left: {
            _$class: msls.ContentItem,
            _$name: "left",
            _$parentName: "columns",
            screen: lightSwitchApplication.ViewComputer,
            data: lightSwitchApplication.tblComputer,
            value: lightSwitchApplication.tblComputer
        },
        serienr: {
            _$class: msls.ContentItem,
            _$name: "serienr",
            _$parentName: "left",
            screen: lightSwitchApplication.ViewComputer,
            data: lightSwitchApplication.tblComputer,
            value: String
        },
        tblHW: {
            _$class: msls.ContentItem,
            _$name: "tblHW",
            _$parentName: "left",
            screen: lightSwitchApplication.ViewComputer,
            data: lightSwitchApplication.tblComputer,
            value: lightSwitchApplication.tblHW
        },
        tblBruger: {
            _$class: msls.ContentItem,
            _$name: "tblBruger",
            _$parentName: "left",
            screen: lightSwitchApplication.ViewComputer,
            data: lightSwitchApplication.tblComputer,
            value: lightSwitchApplication.tblBruger
        },
        note: {
            _$class: msls.ContentItem,
            _$name: "note",
            _$parentName: "left",
            screen: lightSwitchApplication.ViewComputer,
            data: lightSwitchApplication.tblComputer,
            value: String
        },
        right: {
            _$class: msls.ContentItem,
            _$name: "right",
            _$parentName: "columns",
            screen: lightSwitchApplication.ViewComputer,
            data: lightSwitchApplication.tblComputer,
            value: lightSwitchApplication.tblComputer
        },
        CreatedBy: {
            _$class: msls.ContentItem,
            _$name: "CreatedBy",
            _$parentName: "right",
            screen: lightSwitchApplication.ViewComputer,
            data: lightSwitchApplication.tblComputer,
            value: String
        },
        Created: {
            _$class: msls.ContentItem,
            _$name: "Created",
            _$parentName: "right",
            screen: lightSwitchApplication.ViewComputer,
            data: lightSwitchApplication.tblComputer,
            value: Date
        },
        ModifiedBy: {
            _$class: msls.ContentItem,
            _$name: "ModifiedBy",
            _$parentName: "right",
            screen: lightSwitchApplication.ViewComputer,
            data: lightSwitchApplication.tblComputer,
            value: String
        },
        Modified: {
            _$class: msls.ContentItem,
            _$name: "Modified",
            _$parentName: "right",
            screen: lightSwitchApplication.ViewComputer,
            data: lightSwitchApplication.tblComputer,
            value: Date
        },
        Popups: {
            _$class: msls.ContentItem,
            _$name: "Popups",
            _$parentName: "RootContentItem",
            screen: lightSwitchApplication.ViewComputer
        }
    };

    msls._addEntryPoints(lightSwitchApplication.ViewComputer, {
        /// <field>
        /// Called when a new ViewComputer screen is created.
        /// <br/>created(msls.application.ViewComputer screen)
        /// </field>
        created: [lightSwitchApplication.ViewComputer],
        /// <field>
        /// Called before changes on an active ViewComputer screen are applied.
        /// <br/>beforeApplyChanges(msls.application.ViewComputer screen)
        /// </field>
        beforeApplyChanges: [lightSwitchApplication.ViewComputer],
        /// <field>
        /// Called after the Details content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        Details_postRender: [$element, function () { return new lightSwitchApplication.ViewComputer().findContentItem("Details"); }],
        /// <field>
        /// Called after the columns content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        columns_postRender: [$element, function () { return new lightSwitchApplication.ViewComputer().findContentItem("columns"); }],
        /// <field>
        /// Called after the left content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        left_postRender: [$element, function () { return new lightSwitchApplication.ViewComputer().findContentItem("left"); }],
        /// <field>
        /// Called after the serienr content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        serienr_postRender: [$element, function () { return new lightSwitchApplication.ViewComputer().findContentItem("serienr"); }],
        /// <field>
        /// Called after the tblHW content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        tblHW_postRender: [$element, function () { return new lightSwitchApplication.ViewComputer().findContentItem("tblHW"); }],
        /// <field>
        /// Called after the tblBruger content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        tblBruger_postRender: [$element, function () { return new lightSwitchApplication.ViewComputer().findContentItem("tblBruger"); }],
        /// <field>
        /// Called after the note content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        note_postRender: [$element, function () { return new lightSwitchApplication.ViewComputer().findContentItem("note"); }],
        /// <field>
        /// Called after the right content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        right_postRender: [$element, function () { return new lightSwitchApplication.ViewComputer().findContentItem("right"); }],
        /// <field>
        /// Called after the CreatedBy content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        CreatedBy_postRender: [$element, function () { return new lightSwitchApplication.ViewComputer().findContentItem("CreatedBy"); }],
        /// <field>
        /// Called after the Created content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        Created_postRender: [$element, function () { return new lightSwitchApplication.ViewComputer().findContentItem("Created"); }],
        /// <field>
        /// Called after the ModifiedBy content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        ModifiedBy_postRender: [$element, function () { return new lightSwitchApplication.ViewComputer().findContentItem("ModifiedBy"); }],
        /// <field>
        /// Called after the Modified content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        Modified_postRender: [$element, function () { return new lightSwitchApplication.ViewComputer().findContentItem("Modified"); }]
    });

    lightSwitchApplication.AddEditHW_Type.prototype._$contentItems = {
        Tabs: {
            _$class: msls.ContentItem,
            _$name: "Tabs",
            _$parentName: "RootContentItem",
            screen: lightSwitchApplication.AddEditHW_Type
        },
        Details: {
            _$class: msls.ContentItem,
            _$name: "Details",
            _$parentName: "Tabs",
            screen: lightSwitchApplication.AddEditHW_Type,
            data: lightSwitchApplication.AddEditHW_Type,
            value: lightSwitchApplication.AddEditHW_Type
        },
        columns: {
            _$class: msls.ContentItem,
            _$name: "columns",
            _$parentName: "Details",
            screen: lightSwitchApplication.AddEditHW_Type,
            data: lightSwitchApplication.AddEditHW_Type,
            value: lightSwitchApplication.tblHWtype
        },
        left: {
            _$class: msls.ContentItem,
            _$name: "left",
            _$parentName: "columns",
            screen: lightSwitchApplication.AddEditHW_Type,
            data: lightSwitchApplication.tblHWtype,
            value: lightSwitchApplication.tblHWtype
        },
        type: {
            _$class: msls.ContentItem,
            _$name: "type",
            _$parentName: "left",
            screen: lightSwitchApplication.AddEditHW_Type,
            data: lightSwitchApplication.tblHWtype,
            value: String
        },
        right: {
            _$class: msls.ContentItem,
            _$name: "right",
            _$parentName: "columns",
            screen: lightSwitchApplication.AddEditHW_Type,
            data: lightSwitchApplication.tblHWtype,
            value: lightSwitchApplication.tblHWtype
        },
        Popups: {
            _$class: msls.ContentItem,
            _$name: "Popups",
            _$parentName: "RootContentItem",
            screen: lightSwitchApplication.AddEditHW_Type
        }
    };

    msls._addEntryPoints(lightSwitchApplication.AddEditHW_Type, {
        /// <field>
        /// Called when a new AddEditHW_Type screen is created.
        /// <br/>created(msls.application.AddEditHW_Type screen)
        /// </field>
        created: [lightSwitchApplication.AddEditHW_Type],
        /// <field>
        /// Called before changes on an active AddEditHW_Type screen are applied.
        /// <br/>beforeApplyChanges(msls.application.AddEditHW_Type screen)
        /// </field>
        beforeApplyChanges: [lightSwitchApplication.AddEditHW_Type],
        /// <field>
        /// Called after the Details content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        Details_postRender: [$element, function () { return new lightSwitchApplication.AddEditHW_Type().findContentItem("Details"); }],
        /// <field>
        /// Called after the columns content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        columns_postRender: [$element, function () { return new lightSwitchApplication.AddEditHW_Type().findContentItem("columns"); }],
        /// <field>
        /// Called after the left content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        left_postRender: [$element, function () { return new lightSwitchApplication.AddEditHW_Type().findContentItem("left"); }],
        /// <field>
        /// Called after the type content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        type_postRender: [$element, function () { return new lightSwitchApplication.AddEditHW_Type().findContentItem("type"); }],
        /// <field>
        /// Called after the right content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        right_postRender: [$element, function () { return new lightSwitchApplication.AddEditHW_Type().findContentItem("right"); }]
    });

    lightSwitchApplication.BrowseHW_Types.prototype._$contentItems = {
        Tabs: {
            _$class: msls.ContentItem,
            _$name: "Tabs",
            _$parentName: "RootContentItem",
            screen: lightSwitchApplication.BrowseHW_Types
        },
        HW_TypeList: {
            _$class: msls.ContentItem,
            _$name: "HW_TypeList",
            _$parentName: "Tabs",
            screen: lightSwitchApplication.BrowseHW_Types,
            data: lightSwitchApplication.BrowseHW_Types,
            value: lightSwitchApplication.BrowseHW_Types
        },
        tblHWtypes: {
            _$class: msls.ContentItem,
            _$name: "tblHWtypes",
            _$parentName: "HW_TypeList",
            screen: lightSwitchApplication.BrowseHW_Types,
            data: lightSwitchApplication.BrowseHW_Types,
            value: {
                _$class: msls.VisualCollection,
                screen: lightSwitchApplication.BrowseHW_Types,
                _$entry: {
                    elementType: lightSwitchApplication.tblHWtype
                }
            }
        },
        rows: {
            _$class: msls.ContentItem,
            _$name: "rows",
            _$parentName: "tblHWtypes",
            screen: lightSwitchApplication.BrowseHW_Types,
            data: lightSwitchApplication.tblHWtype,
            value: lightSwitchApplication.tblHWtype
        },
        type: {
            _$class: msls.ContentItem,
            _$name: "type",
            _$parentName: "rows",
            screen: lightSwitchApplication.BrowseHW_Types,
            data: lightSwitchApplication.tblHWtype,
            value: String
        },
        CreatedBy: {
            _$class: msls.ContentItem,
            _$name: "CreatedBy",
            _$parentName: "rows",
            screen: lightSwitchApplication.BrowseHW_Types,
            data: lightSwitchApplication.tblHWtype,
            value: String
        },
        Created: {
            _$class: msls.ContentItem,
            _$name: "Created",
            _$parentName: "rows",
            screen: lightSwitchApplication.BrowseHW_Types,
            data: lightSwitchApplication.tblHWtype,
            value: Date
        },
        Popups: {
            _$class: msls.ContentItem,
            _$name: "Popups",
            _$parentName: "RootContentItem",
            screen: lightSwitchApplication.BrowseHW_Types
        }
    };

    msls._addEntryPoints(lightSwitchApplication.BrowseHW_Types, {
        /// <field>
        /// Called when a new BrowseHW_Types screen is created.
        /// <br/>created(msls.application.BrowseHW_Types screen)
        /// </field>
        created: [lightSwitchApplication.BrowseHW_Types],
        /// <field>
        /// Called before changes on an active BrowseHW_Types screen are applied.
        /// <br/>beforeApplyChanges(msls.application.BrowseHW_Types screen)
        /// </field>
        beforeApplyChanges: [lightSwitchApplication.BrowseHW_Types],
        /// <field>
        /// Called after the HW_TypeList content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        HW_TypeList_postRender: [$element, function () { return new lightSwitchApplication.BrowseHW_Types().findContentItem("HW_TypeList"); }],
        /// <field>
        /// Called after the tblHWtypes content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        tblHWtypes_postRender: [$element, function () { return new lightSwitchApplication.BrowseHW_Types().findContentItem("tblHWtypes"); }],
        /// <field>
        /// Called after the rows content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        rows_postRender: [$element, function () { return new lightSwitchApplication.BrowseHW_Types().findContentItem("rows"); }],
        /// <field>
        /// Called after the type content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        type_postRender: [$element, function () { return new lightSwitchApplication.BrowseHW_Types().findContentItem("type"); }],
        /// <field>
        /// Called after the CreatedBy content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        CreatedBy_postRender: [$element, function () { return new lightSwitchApplication.BrowseHW_Types().findContentItem("CreatedBy"); }],
        /// <field>
        /// Called after the Created content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        Created_postRender: [$element, function () { return new lightSwitchApplication.BrowseHW_Types().findContentItem("Created"); }]
    });

    lightSwitchApplication.ViewHW_Type.prototype._$contentItems = {
        Tabs: {
            _$class: msls.ContentItem,
            _$name: "Tabs",
            _$parentName: "RootContentItem",
            screen: lightSwitchApplication.ViewHW_Type
        },
        Details: {
            _$class: msls.ContentItem,
            _$name: "Details",
            _$parentName: "Tabs",
            screen: lightSwitchApplication.ViewHW_Type,
            data: lightSwitchApplication.ViewHW_Type,
            value: lightSwitchApplication.ViewHW_Type
        },
        columns: {
            _$class: msls.ContentItem,
            _$name: "columns",
            _$parentName: "Details",
            screen: lightSwitchApplication.ViewHW_Type,
            data: lightSwitchApplication.ViewHW_Type,
            value: lightSwitchApplication.tblHWtype
        },
        left: {
            _$class: msls.ContentItem,
            _$name: "left",
            _$parentName: "columns",
            screen: lightSwitchApplication.ViewHW_Type,
            data: lightSwitchApplication.tblHWtype,
            value: lightSwitchApplication.tblHWtype
        },
        type: {
            _$class: msls.ContentItem,
            _$name: "type",
            _$parentName: "left",
            screen: lightSwitchApplication.ViewHW_Type,
            data: lightSwitchApplication.tblHWtype,
            value: String
        },
        CreatedBy: {
            _$class: msls.ContentItem,
            _$name: "CreatedBy",
            _$parentName: "left",
            screen: lightSwitchApplication.ViewHW_Type,
            data: lightSwitchApplication.tblHWtype,
            value: String
        },
        Created: {
            _$class: msls.ContentItem,
            _$name: "Created",
            _$parentName: "left",
            screen: lightSwitchApplication.ViewHW_Type,
            data: lightSwitchApplication.tblHWtype,
            value: Date
        },
        right: {
            _$class: msls.ContentItem,
            _$name: "right",
            _$parentName: "columns",
            screen: lightSwitchApplication.ViewHW_Type,
            data: lightSwitchApplication.tblHWtype,
            value: lightSwitchApplication.tblHWtype
        },
        ModifiedBy: {
            _$class: msls.ContentItem,
            _$name: "ModifiedBy",
            _$parentName: "right",
            screen: lightSwitchApplication.ViewHW_Type,
            data: lightSwitchApplication.tblHWtype,
            value: String
        },
        Modified: {
            _$class: msls.ContentItem,
            _$name: "Modified",
            _$parentName: "right",
            screen: lightSwitchApplication.ViewHW_Type,
            data: lightSwitchApplication.tblHWtype,
            value: Date
        },
        Popups: {
            _$class: msls.ContentItem,
            _$name: "Popups",
            _$parentName: "RootContentItem",
            screen: lightSwitchApplication.ViewHW_Type
        }
    };

    msls._addEntryPoints(lightSwitchApplication.ViewHW_Type, {
        /// <field>
        /// Called when a new ViewHW_Type screen is created.
        /// <br/>created(msls.application.ViewHW_Type screen)
        /// </field>
        created: [lightSwitchApplication.ViewHW_Type],
        /// <field>
        /// Called before changes on an active ViewHW_Type screen are applied.
        /// <br/>beforeApplyChanges(msls.application.ViewHW_Type screen)
        /// </field>
        beforeApplyChanges: [lightSwitchApplication.ViewHW_Type],
        /// <field>
        /// Called after the Details content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        Details_postRender: [$element, function () { return new lightSwitchApplication.ViewHW_Type().findContentItem("Details"); }],
        /// <field>
        /// Called after the columns content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        columns_postRender: [$element, function () { return new lightSwitchApplication.ViewHW_Type().findContentItem("columns"); }],
        /// <field>
        /// Called after the left content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        left_postRender: [$element, function () { return new lightSwitchApplication.ViewHW_Type().findContentItem("left"); }],
        /// <field>
        /// Called after the type content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        type_postRender: [$element, function () { return new lightSwitchApplication.ViewHW_Type().findContentItem("type"); }],
        /// <field>
        /// Called after the CreatedBy content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        CreatedBy_postRender: [$element, function () { return new lightSwitchApplication.ViewHW_Type().findContentItem("CreatedBy"); }],
        /// <field>
        /// Called after the Created content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        Created_postRender: [$element, function () { return new lightSwitchApplication.ViewHW_Type().findContentItem("Created"); }],
        /// <field>
        /// Called after the right content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        right_postRender: [$element, function () { return new lightSwitchApplication.ViewHW_Type().findContentItem("right"); }],
        /// <field>
        /// Called after the ModifiedBy content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        ModifiedBy_postRender: [$element, function () { return new lightSwitchApplication.ViewHW_Type().findContentItem("ModifiedBy"); }],
        /// <field>
        /// Called after the Modified content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        Modified_postRender: [$element, function () { return new lightSwitchApplication.ViewHW_Type().findContentItem("Modified"); }]
    });

    lightSwitchApplication.AddEditHW.prototype._$contentItems = {
        Tabs: {
            _$class: msls.ContentItem,
            _$name: "Tabs",
            _$parentName: "RootContentItem",
            screen: lightSwitchApplication.AddEditHW
        },
        Details: {
            _$class: msls.ContentItem,
            _$name: "Details",
            _$parentName: "Tabs",
            screen: lightSwitchApplication.AddEditHW,
            data: lightSwitchApplication.AddEditHW,
            value: lightSwitchApplication.AddEditHW
        },
        columns: {
            _$class: msls.ContentItem,
            _$name: "columns",
            _$parentName: "Details",
            screen: lightSwitchApplication.AddEditHW,
            data: lightSwitchApplication.AddEditHW,
            value: lightSwitchApplication.tblHW
        },
        left: {
            _$class: msls.ContentItem,
            _$name: "left",
            _$parentName: "columns",
            screen: lightSwitchApplication.AddEditHW,
            data: lightSwitchApplication.tblHW,
            value: lightSwitchApplication.tblHW
        },
        hw: {
            _$class: msls.ContentItem,
            _$name: "hw",
            _$parentName: "left",
            screen: lightSwitchApplication.AddEditHW,
            data: lightSwitchApplication.tblHW,
            value: String
        },
        navn: {
            _$class: msls.ContentItem,
            _$name: "navn",
            _$parentName: "left",
            screen: lightSwitchApplication.AddEditHW,
            data: lightSwitchApplication.tblHW,
            value: String
        },
        fabrikat: {
            _$class: msls.ContentItem,
            _$name: "fabrikat",
            _$parentName: "left",
            screen: lightSwitchApplication.AddEditHW,
            data: lightSwitchApplication.tblHW,
            value: String
        },
        tblHWtype: {
            _$class: msls.ContentItem,
            _$name: "tblHWtype",
            _$parentName: "left",
            screen: lightSwitchApplication.AddEditHW,
            data: lightSwitchApplication.tblHW,
            value: lightSwitchApplication.tblHWtype
        },
        RowTemplate: {
            _$class: msls.ContentItem,
            _$name: "RowTemplate",
            _$parentName: "tblHWtype",
            screen: lightSwitchApplication.AddEditHW,
            data: lightSwitchApplication.tblHWtype,
            value: lightSwitchApplication.tblHWtype
        },
        right: {
            _$class: msls.ContentItem,
            _$name: "right",
            _$parentName: "columns",
            screen: lightSwitchApplication.AddEditHW,
            data: lightSwitchApplication.tblHW,
            value: lightSwitchApplication.tblHW
        },
        Popups: {
            _$class: msls.ContentItem,
            _$name: "Popups",
            _$parentName: "RootContentItem",
            screen: lightSwitchApplication.AddEditHW
        }
    };

    msls._addEntryPoints(lightSwitchApplication.AddEditHW, {
        /// <field>
        /// Called when a new AddEditHW screen is created.
        /// <br/>created(msls.application.AddEditHW screen)
        /// </field>
        created: [lightSwitchApplication.AddEditHW],
        /// <field>
        /// Called before changes on an active AddEditHW screen are applied.
        /// <br/>beforeApplyChanges(msls.application.AddEditHW screen)
        /// </field>
        beforeApplyChanges: [lightSwitchApplication.AddEditHW],
        /// <field>
        /// Called after the Details content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        Details_postRender: [$element, function () { return new lightSwitchApplication.AddEditHW().findContentItem("Details"); }],
        /// <field>
        /// Called after the columns content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        columns_postRender: [$element, function () { return new lightSwitchApplication.AddEditHW().findContentItem("columns"); }],
        /// <field>
        /// Called after the left content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        left_postRender: [$element, function () { return new lightSwitchApplication.AddEditHW().findContentItem("left"); }],
        /// <field>
        /// Called after the hw content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        hw_postRender: [$element, function () { return new lightSwitchApplication.AddEditHW().findContentItem("hw"); }],
        /// <field>
        /// Called after the navn content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        navn_postRender: [$element, function () { return new lightSwitchApplication.AddEditHW().findContentItem("navn"); }],
        /// <field>
        /// Called after the fabrikat content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        fabrikat_postRender: [$element, function () { return new lightSwitchApplication.AddEditHW().findContentItem("fabrikat"); }],
        /// <field>
        /// Called after the tblHWtype content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        tblHWtype_postRender: [$element, function () { return new lightSwitchApplication.AddEditHW().findContentItem("tblHWtype"); }],
        /// <field>
        /// Called after the RowTemplate content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        RowTemplate_postRender: [$element, function () { return new lightSwitchApplication.AddEditHW().findContentItem("RowTemplate"); }],
        /// <field>
        /// Called after the right content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        right_postRender: [$element, function () { return new lightSwitchApplication.AddEditHW().findContentItem("right"); }]
    });

    lightSwitchApplication.BrowseHWs.prototype._$contentItems = {
        Tabs: {
            _$class: msls.ContentItem,
            _$name: "Tabs",
            _$parentName: "RootContentItem",
            screen: lightSwitchApplication.BrowseHWs
        },
        HWList: {
            _$class: msls.ContentItem,
            _$name: "HWList",
            _$parentName: "Tabs",
            screen: lightSwitchApplication.BrowseHWs,
            data: lightSwitchApplication.BrowseHWs,
            value: lightSwitchApplication.BrowseHWs
        },
        tblHWs: {
            _$class: msls.ContentItem,
            _$name: "tblHWs",
            _$parentName: "HWList",
            screen: lightSwitchApplication.BrowseHWs,
            data: lightSwitchApplication.BrowseHWs,
            value: {
                _$class: msls.VisualCollection,
                screen: lightSwitchApplication.BrowseHWs,
                _$entry: {
                    elementType: lightSwitchApplication.tblHW
                }
            }
        },
        rows: {
            _$class: msls.ContentItem,
            _$name: "rows",
            _$parentName: "tblHWs",
            screen: lightSwitchApplication.BrowseHWs,
            data: lightSwitchApplication.tblHW,
            value: lightSwitchApplication.tblHW
        },
        hw: {
            _$class: msls.ContentItem,
            _$name: "hw",
            _$parentName: "rows",
            screen: lightSwitchApplication.BrowseHWs,
            data: lightSwitchApplication.tblHW,
            value: String
        },
        navn: {
            _$class: msls.ContentItem,
            _$name: "navn",
            _$parentName: "rows",
            screen: lightSwitchApplication.BrowseHWs,
            data: lightSwitchApplication.tblHW,
            value: String
        },
        fabrikat: {
            _$class: msls.ContentItem,
            _$name: "fabrikat",
            _$parentName: "rows",
            screen: lightSwitchApplication.BrowseHWs,
            data: lightSwitchApplication.tblHW,
            value: String
        },
        tblHWtype: {
            _$class: msls.ContentItem,
            _$name: "tblHWtype",
            _$parentName: "rows",
            screen: lightSwitchApplication.BrowseHWs,
            data: lightSwitchApplication.tblHW,
            value: lightSwitchApplication.tblHWtype
        },
        Popups: {
            _$class: msls.ContentItem,
            _$name: "Popups",
            _$parentName: "RootContentItem",
            screen: lightSwitchApplication.BrowseHWs
        }
    };

    msls._addEntryPoints(lightSwitchApplication.BrowseHWs, {
        /// <field>
        /// Called when a new BrowseHWs screen is created.
        /// <br/>created(msls.application.BrowseHWs screen)
        /// </field>
        created: [lightSwitchApplication.BrowseHWs],
        /// <field>
        /// Called before changes on an active BrowseHWs screen are applied.
        /// <br/>beforeApplyChanges(msls.application.BrowseHWs screen)
        /// </field>
        beforeApplyChanges: [lightSwitchApplication.BrowseHWs],
        /// <field>
        /// Called after the HWList content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        HWList_postRender: [$element, function () { return new lightSwitchApplication.BrowseHWs().findContentItem("HWList"); }],
        /// <field>
        /// Called after the tblHWs content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        tblHWs_postRender: [$element, function () { return new lightSwitchApplication.BrowseHWs().findContentItem("tblHWs"); }],
        /// <field>
        /// Called after the rows content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        rows_postRender: [$element, function () { return new lightSwitchApplication.BrowseHWs().findContentItem("rows"); }],
        /// <field>
        /// Called after the hw content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        hw_postRender: [$element, function () { return new lightSwitchApplication.BrowseHWs().findContentItem("hw"); }],
        /// <field>
        /// Called after the navn content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        navn_postRender: [$element, function () { return new lightSwitchApplication.BrowseHWs().findContentItem("navn"); }],
        /// <field>
        /// Called after the fabrikat content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        fabrikat_postRender: [$element, function () { return new lightSwitchApplication.BrowseHWs().findContentItem("fabrikat"); }],
        /// <field>
        /// Called after the tblHWtype content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        tblHWtype_postRender: [$element, function () { return new lightSwitchApplication.BrowseHWs().findContentItem("tblHWtype"); }]
    });

    lightSwitchApplication.ViewHW.prototype._$contentItems = {
        Tabs: {
            _$class: msls.ContentItem,
            _$name: "Tabs",
            _$parentName: "RootContentItem",
            screen: lightSwitchApplication.ViewHW
        },
        Details: {
            _$class: msls.ContentItem,
            _$name: "Details",
            _$parentName: "Tabs",
            screen: lightSwitchApplication.ViewHW,
            data: lightSwitchApplication.ViewHW,
            value: lightSwitchApplication.ViewHW
        },
        columns: {
            _$class: msls.ContentItem,
            _$name: "columns",
            _$parentName: "Details",
            screen: lightSwitchApplication.ViewHW,
            data: lightSwitchApplication.ViewHW,
            value: lightSwitchApplication.tblHW
        },
        left: {
            _$class: msls.ContentItem,
            _$name: "left",
            _$parentName: "columns",
            screen: lightSwitchApplication.ViewHW,
            data: lightSwitchApplication.tblHW,
            value: lightSwitchApplication.tblHW
        },
        hw: {
            _$class: msls.ContentItem,
            _$name: "hw",
            _$parentName: "left",
            screen: lightSwitchApplication.ViewHW,
            data: lightSwitchApplication.tblHW,
            value: String
        },
        navn: {
            _$class: msls.ContentItem,
            _$name: "navn",
            _$parentName: "left",
            screen: lightSwitchApplication.ViewHW,
            data: lightSwitchApplication.tblHW,
            value: String
        },
        fabrikat: {
            _$class: msls.ContentItem,
            _$name: "fabrikat",
            _$parentName: "left",
            screen: lightSwitchApplication.ViewHW,
            data: lightSwitchApplication.tblHW,
            value: String
        },
        tblHWtype: {
            _$class: msls.ContentItem,
            _$name: "tblHWtype",
            _$parentName: "left",
            screen: lightSwitchApplication.ViewHW,
            data: lightSwitchApplication.tblHW,
            value: lightSwitchApplication.tblHWtype
        },
        right: {
            _$class: msls.ContentItem,
            _$name: "right",
            _$parentName: "columns",
            screen: lightSwitchApplication.ViewHW,
            data: lightSwitchApplication.tblHW,
            value: lightSwitchApplication.tblHW
        },
        CreatedBy: {
            _$class: msls.ContentItem,
            _$name: "CreatedBy",
            _$parentName: "right",
            screen: lightSwitchApplication.ViewHW,
            data: lightSwitchApplication.tblHW,
            value: String
        },
        Created: {
            _$class: msls.ContentItem,
            _$name: "Created",
            _$parentName: "right",
            screen: lightSwitchApplication.ViewHW,
            data: lightSwitchApplication.tblHW,
            value: Date
        },
        ModifiedBy: {
            _$class: msls.ContentItem,
            _$name: "ModifiedBy",
            _$parentName: "right",
            screen: lightSwitchApplication.ViewHW,
            data: lightSwitchApplication.tblHW,
            value: String
        },
        Modified: {
            _$class: msls.ContentItem,
            _$name: "Modified",
            _$parentName: "right",
            screen: lightSwitchApplication.ViewHW,
            data: lightSwitchApplication.tblHW,
            value: Date
        },
        Popups: {
            _$class: msls.ContentItem,
            _$name: "Popups",
            _$parentName: "RootContentItem",
            screen: lightSwitchApplication.ViewHW
        }
    };

    msls._addEntryPoints(lightSwitchApplication.ViewHW, {
        /// <field>
        /// Called when a new ViewHW screen is created.
        /// <br/>created(msls.application.ViewHW screen)
        /// </field>
        created: [lightSwitchApplication.ViewHW],
        /// <field>
        /// Called before changes on an active ViewHW screen are applied.
        /// <br/>beforeApplyChanges(msls.application.ViewHW screen)
        /// </field>
        beforeApplyChanges: [lightSwitchApplication.ViewHW],
        /// <field>
        /// Called after the Details content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        Details_postRender: [$element, function () { return new lightSwitchApplication.ViewHW().findContentItem("Details"); }],
        /// <field>
        /// Called after the columns content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        columns_postRender: [$element, function () { return new lightSwitchApplication.ViewHW().findContentItem("columns"); }],
        /// <field>
        /// Called after the left content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        left_postRender: [$element, function () { return new lightSwitchApplication.ViewHW().findContentItem("left"); }],
        /// <field>
        /// Called after the hw content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        hw_postRender: [$element, function () { return new lightSwitchApplication.ViewHW().findContentItem("hw"); }],
        /// <field>
        /// Called after the navn content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        navn_postRender: [$element, function () { return new lightSwitchApplication.ViewHW().findContentItem("navn"); }],
        /// <field>
        /// Called after the fabrikat content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        fabrikat_postRender: [$element, function () { return new lightSwitchApplication.ViewHW().findContentItem("fabrikat"); }],
        /// <field>
        /// Called after the tblHWtype content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        tblHWtype_postRender: [$element, function () { return new lightSwitchApplication.ViewHW().findContentItem("tblHWtype"); }],
        /// <field>
        /// Called after the right content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        right_postRender: [$element, function () { return new lightSwitchApplication.ViewHW().findContentItem("right"); }],
        /// <field>
        /// Called after the CreatedBy content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        CreatedBy_postRender: [$element, function () { return new lightSwitchApplication.ViewHW().findContentItem("CreatedBy"); }],
        /// <field>
        /// Called after the Created content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        Created_postRender: [$element, function () { return new lightSwitchApplication.ViewHW().findContentItem("Created"); }],
        /// <field>
        /// Called after the ModifiedBy content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        ModifiedBy_postRender: [$element, function () { return new lightSwitchApplication.ViewHW().findContentItem("ModifiedBy"); }],
        /// <field>
        /// Called after the Modified content item has been rendered.
        /// <br/>postRender(HTMLElement element, msls.ContentItem contentItem)
        /// </field>
        Modified_postRender: [$element, function () { return new lightSwitchApplication.ViewHW().findContentItem("Modified"); }]
    });

}(msls.application));