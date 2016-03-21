/// <reference path="../Scripts/msls.js" />

window.myapp = msls.application;

(function (lightSwitchApplication) {

    var $Entity = msls.Entity,
        $DataService = msls.DataService,
        $DataWorkspace = msls.DataWorkspace,
        $defineEntity = msls._defineEntity,
        $defineDataService = msls._defineDataService,
        $defineDataWorkspace = msls._defineDataWorkspace,
        $DataServiceQuery = msls.DataServiceQuery,
        $toODataString = msls._toODataString;

    function tblBruger(entitySet) {
        /// <summary>
        /// Represents the tblBruger entity type.
        /// </summary>
        /// <param name="entitySet" type="msls.EntitySet" optional="true">
        /// The entity set that should contain this tblBruger.
        /// </param>
        /// <field name="Id" type="Number">
        /// Gets or sets the id for this tblBruger.
        /// </field>
        /// <field name="initialer" type="String">
        /// Gets or sets the initialer for this tblBruger.
        /// </field>
        /// <field name="navn" type="String">
        /// Gets or sets the navn for this tblBruger.
        /// </field>
        /// <field name="telefon" type="String">
        /// Gets or sets the telefon for this tblBruger.
        /// </field>
        /// <field name="mobil" type="String">
        /// Gets or sets the mobil for this tblBruger.
        /// </field>
        /// <field name="email" type="String">
        /// Gets or sets the email for this tblBruger.
        /// </field>
        /// <field name="statsaut" type="Boolean">
        /// Gets or sets the statsaut for this tblBruger.
        /// </field>
        /// <field name="partner" type="Boolean">
        /// Gets or sets the partner for this tblBruger.
        /// </field>
        /// <field name="tblComputers" type="msls.EntityCollection" elementType="msls.application.tblComputer">
        /// Gets the tblComputers for this tblBruger.
        /// </field>
        /// <field name="CreatedBy" type="String">
        /// Gets or sets the createdBy for this tblBruger.
        /// </field>
        /// <field name="Created" type="Date">
        /// Gets or sets the created for this tblBruger.
        /// </field>
        /// <field name="ModifiedBy" type="String">
        /// Gets or sets the modifiedBy for this tblBruger.
        /// </field>
        /// <field name="Modified" type="Date">
        /// Gets or sets the modified for this tblBruger.
        /// </field>
        /// <field name="RowVersion" type="Array">
        /// Gets or sets the rowVersion for this tblBruger.
        /// </field>
        /// <field name="details" type="msls.application.tblBruger.Details">
        /// Gets the details for this tblBruger.
        /// </field>
        $Entity.call(this, entitySet);
    }

    function tblComputer(entitySet) {
        /// <summary>
        /// Represents the tblComputer entity type.
        /// </summary>
        /// <param name="entitySet" type="msls.EntitySet" optional="true">
        /// The entity set that should contain this tblComputer.
        /// </param>
        /// <field name="Id" type="Number">
        /// Gets or sets the id for this tblComputer.
        /// </field>
        /// <field name="serienr" type="String">
        /// Gets or sets the serienr for this tblComputer.
        /// </field>
        /// <field name="note" type="String">
        /// Gets or sets the note for this tblComputer.
        /// </field>
        /// <field name="tblHW" type="msls.application.tblHW">
        /// Gets or sets the tblHW for this tblComputer.
        /// </field>
        /// <field name="tblBruger" type="msls.application.tblBruger">
        /// Gets or sets the tblBruger for this tblComputer.
        /// </field>
        /// <field name="CreatedBy" type="String">
        /// Gets or sets the createdBy for this tblComputer.
        /// </field>
        /// <field name="Created" type="Date">
        /// Gets or sets the created for this tblComputer.
        /// </field>
        /// <field name="ModifiedBy" type="String">
        /// Gets or sets the modifiedBy for this tblComputer.
        /// </field>
        /// <field name="Modified" type="Date">
        /// Gets or sets the modified for this tblComputer.
        /// </field>
        /// <field name="RowVersion" type="Array">
        /// Gets or sets the rowVersion for this tblComputer.
        /// </field>
        /// <field name="details" type="msls.application.tblComputer.Details">
        /// Gets the details for this tblComputer.
        /// </field>
        $Entity.call(this, entitySet);
    }

    function tblHWtype(entitySet) {
        /// <summary>
        /// Represents the tblHWtype entity type.
        /// </summary>
        /// <param name="entitySet" type="msls.EntitySet" optional="true">
        /// The entity set that should contain this tblHWtype.
        /// </param>
        /// <field name="Id" type="Number">
        /// Gets or sets the id for this tblHWtype.
        /// </field>
        /// <field name="type" type="String">
        /// Gets or sets the type for this tblHWtype.
        /// </field>
        /// <field name="tblHWs" type="msls.EntityCollection" elementType="msls.application.tblHW">
        /// Gets the tblHWs for this tblHWtype.
        /// </field>
        /// <field name="CreatedBy" type="String">
        /// Gets or sets the createdBy for this tblHWtype.
        /// </field>
        /// <field name="Created" type="Date">
        /// Gets or sets the created for this tblHWtype.
        /// </field>
        /// <field name="ModifiedBy" type="String">
        /// Gets or sets the modifiedBy for this tblHWtype.
        /// </field>
        /// <field name="Modified" type="Date">
        /// Gets or sets the modified for this tblHWtype.
        /// </field>
        /// <field name="RowVersion" type="Array">
        /// Gets or sets the rowVersion for this tblHWtype.
        /// </field>
        /// <field name="details" type="msls.application.tblHWtype.Details">
        /// Gets the details for this tblHWtype.
        /// </field>
        $Entity.call(this, entitySet);
    }

    function tblHW(entitySet) {
        /// <summary>
        /// Represents the tblHW entity type.
        /// </summary>
        /// <param name="entitySet" type="msls.EntitySet" optional="true">
        /// The entity set that should contain this tblHW.
        /// </param>
        /// <field name="Id" type="Number">
        /// Gets or sets the id for this tblHW.
        /// </field>
        /// <field name="hw" type="String">
        /// Gets or sets the hw for this tblHW.
        /// </field>
        /// <field name="navn" type="String">
        /// Gets or sets the navn for this tblHW.
        /// </field>
        /// <field name="fabrikat" type="String">
        /// Gets or sets the fabrikat for this tblHW.
        /// </field>
        /// <field name="tblHWtype" type="msls.application.tblHWtype">
        /// Gets or sets the tblHWtype for this tblHW.
        /// </field>
        /// <field name="tblComputers" type="msls.EntityCollection" elementType="msls.application.tblComputer">
        /// Gets the tblComputers for this tblHW.
        /// </field>
        /// <field name="CreatedBy" type="String">
        /// Gets or sets the createdBy for this tblHW.
        /// </field>
        /// <field name="Created" type="Date">
        /// Gets or sets the created for this tblHW.
        /// </field>
        /// <field name="ModifiedBy" type="String">
        /// Gets or sets the modifiedBy for this tblHW.
        /// </field>
        /// <field name="Modified" type="Date">
        /// Gets or sets the modified for this tblHW.
        /// </field>
        /// <field name="RowVersion" type="Array">
        /// Gets or sets the rowVersion for this tblHW.
        /// </field>
        /// <field name="details" type="msls.application.tblHW.Details">
        /// Gets the details for this tblHW.
        /// </field>
        $Entity.call(this, entitySet);
    }

    function ApplicationData(dataWorkspace) {
        /// <summary>
        /// Represents the ApplicationData data service.
        /// </summary>
        /// <param name="dataWorkspace" type="msls.DataWorkspace">
        /// The data workspace that created this data service.
        /// </param>
        /// <field name="tblBrugers" type="msls.EntitySet">
        /// Gets the tblBrugers entity set.
        /// </field>
        /// <field name="tblComputers" type="msls.EntitySet">
        /// Gets the tblComputers entity set.
        /// </field>
        /// <field name="tblHWtypes" type="msls.EntitySet">
        /// Gets the tblHWtypes entity set.
        /// </field>
        /// <field name="tblHWs" type="msls.EntitySet">
        /// Gets the tblHWs entity set.
        /// </field>
        /// <field name="details" type="msls.application.ApplicationData.Details">
        /// Gets the details for this data service.
        /// </field>
        $DataService.call(this, dataWorkspace);
    };
    function DataWorkspace() {
        /// <summary>
        /// Represents the data workspace.
        /// </summary>
        /// <field name="ApplicationData" type="msls.application.ApplicationData">
        /// Gets the ApplicationData data service.
        /// </field>
        /// <field name="details" type="msls.application.DataWorkspace.Details">
        /// Gets the details for this data workspace.
        /// </field>
        $DataWorkspace.call(this);
    };

    msls._addToNamespace("msls.application", {

        tblBruger: $defineEntity(tblBruger, [
            { name: "Id", type: Number },
            { name: "initialer", type: String },
            { name: "navn", type: String },
            { name: "telefon", type: String },
            { name: "mobil", type: String },
            { name: "email", type: String },
            { name: "statsaut", type: Boolean },
            { name: "partner", type: Boolean },
            { name: "tblComputers", kind: "collection", elementType: tblComputer },
            { name: "CreatedBy", type: String, isReadOnly: true },
            { name: "Created", type: Date, isReadOnly: true },
            { name: "ModifiedBy", type: String, isReadOnly: true },
            { name: "Modified", type: Date, isReadOnly: true },
            { name: "RowVersion", type: Array }
        ]),

        tblComputer: $defineEntity(tblComputer, [
            { name: "Id", type: Number },
            { name: "serienr", type: String },
            { name: "note", type: String },
            { name: "tblHW", kind: "reference", type: tblHW },
            { name: "tblBruger", kind: "reference", type: tblBruger },
            { name: "CreatedBy", type: String, isReadOnly: true },
            { name: "Created", type: Date, isReadOnly: true },
            { name: "ModifiedBy", type: String, isReadOnly: true },
            { name: "Modified", type: Date, isReadOnly: true },
            { name: "RowVersion", type: Array }
        ]),

        tblHWtype: $defineEntity(tblHWtype, [
            { name: "Id", type: Number },
            { name: "type", type: String },
            { name: "tblHWs", kind: "collection", elementType: tblHW },
            { name: "CreatedBy", type: String, isReadOnly: true },
            { name: "Created", type: Date, isReadOnly: true },
            { name: "ModifiedBy", type: String, isReadOnly: true },
            { name: "Modified", type: Date, isReadOnly: true },
            { name: "RowVersion", type: Array }
        ]),

        tblHW: $defineEntity(tblHW, [
            { name: "Id", type: Number },
            { name: "hw", type: String },
            { name: "navn", type: String },
            { name: "fabrikat", type: String },
            { name: "tblHWtype", kind: "reference", type: tblHWtype },
            { name: "tblComputers", kind: "collection", elementType: tblComputer },
            { name: "CreatedBy", type: String, isReadOnly: true },
            { name: "Created", type: Date, isReadOnly: true },
            { name: "ModifiedBy", type: String, isReadOnly: true },
            { name: "Modified", type: Date, isReadOnly: true },
            { name: "RowVersion", type: Array }
        ]),

        ApplicationData: $defineDataService(ApplicationData, lightSwitchApplication.rootUri + "/ApplicationData.svc", [
            { name: "tblBrugers", elementType: tblBruger },
            { name: "tblComputers", elementType: tblComputer },
            { name: "tblHWtypes", elementType: tblHWtype },
            { name: "tblHWs", elementType: tblHW }
        ], [
            {
                name: "tblBrugers_SingleOrDefault", value: function (Id) {
                    return new $DataServiceQuery({ _entitySet: this.tblBrugers },
                        lightSwitchApplication.rootUri + "/ApplicationData.svc" + "/tblBrugers(" + "Id=" + $toODataString(Id, "Int32?") + ")"
                    );
                }
            },
            {
                name: "tblComputers_SingleOrDefault", value: function (Id) {
                    return new $DataServiceQuery({ _entitySet: this.tblComputers },
                        lightSwitchApplication.rootUri + "/ApplicationData.svc" + "/tblComputers(" + "Id=" + $toODataString(Id, "Int32?") + ")"
                    );
                }
            },
            {
                name: "tblHWtypes_SingleOrDefault", value: function (Id) {
                    return new $DataServiceQuery({ _entitySet: this.tblHWtypes },
                        lightSwitchApplication.rootUri + "/ApplicationData.svc" + "/tblHWtypes(" + "Id=" + $toODataString(Id, "Int32?") + ")"
                    );
                }
            },
            {
                name: "tblHWs_SingleOrDefault", value: function (Id) {
                    return new $DataServiceQuery({ _entitySet: this.tblHWs },
                        lightSwitchApplication.rootUri + "/ApplicationData.svc" + "/tblHWs(" + "Id=" + $toODataString(Id, "Int32?") + ")"
                    );
                }
            }
        ]),

        DataWorkspace: $defineDataWorkspace(DataWorkspace, [
            { name: "ApplicationData", type: ApplicationData }
        ])

    });

}(msls.application));
