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
        /// <field name="tblLokale" type="msls.application.tblLokale">
        /// Gets or sets the tblLokale for this tblBruger.
        /// </field>
        /// <field name="tblBrugerRelations" type="msls.EntityCollection" elementType="msls.application.tblBrugerRelation">
        /// Gets the tblBrugerRelations for this tblBruger.
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
        /// <field name="tblFeatureRelations" type="msls.EntityCollection" elementType="msls.application.tblFeatureRelation">
        /// Gets the tblFeatureRelations for this tblComputer.
        /// </field>
        /// <field name="tblIps" type="msls.EntityCollection" elementType="msls.application.tblIp">
        /// Gets the tblIps for this tblComputer.
        /// </field>
        /// <field name="tblBrugerRelations" type="msls.EntityCollection" elementType="msls.application.tblBrugerRelation">
        /// Gets the tblBrugerRelations for this tblComputer.
        /// </field>
        /// <field name="tblLokale" type="msls.application.tblLokale">
        /// Gets or sets the tblLokale for this tblComputer.
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

    function tblFeatureRelation(entitySet) {
        /// <summary>
        /// Represents the tblFeatureRelation entity type.
        /// </summary>
        /// <param name="entitySet" type="msls.EntitySet" optional="true">
        /// The entity set that should contain this tblFeatureRelation.
        /// </param>
        /// <field name="Id" type="Number">
        /// Gets or sets the id for this tblFeatureRelation.
        /// </field>
        /// <field name="tblFeature" type="msls.application.tblFeature">
        /// Gets or sets the tblFeature for this tblFeatureRelation.
        /// </field>
        /// <field name="tblComputer" type="msls.application.tblComputer">
        /// Gets or sets the tblComputer for this tblFeatureRelation.
        /// </field>
        /// <field name="CreatedBy" type="String">
        /// Gets or sets the createdBy for this tblFeatureRelation.
        /// </field>
        /// <field name="Created" type="Date">
        /// Gets or sets the created for this tblFeatureRelation.
        /// </field>
        /// <field name="ModifiedBy" type="String">
        /// Gets or sets the modifiedBy for this tblFeatureRelation.
        /// </field>
        /// <field name="Modified" type="Date">
        /// Gets or sets the modified for this tblFeatureRelation.
        /// </field>
        /// <field name="RowVersion" type="Array">
        /// Gets or sets the rowVersion for this tblFeatureRelation.
        /// </field>
        /// <field name="details" type="msls.application.tblFeatureRelation.Details">
        /// Gets the details for this tblFeatureRelation.
        /// </field>
        $Entity.call(this, entitySet);
    }

    function tblFeature(entitySet) {
        /// <summary>
        /// Represents the tblFeature entity type.
        /// </summary>
        /// <param name="entitySet" type="msls.EntitySet" optional="true">
        /// The entity set that should contain this tblFeature.
        /// </param>
        /// <field name="Id" type="Number">
        /// Gets or sets the id for this tblFeature.
        /// </field>
        /// <field name="navn" type="String">
        /// Gets or sets the navn for this tblFeature.
        /// </field>
        /// <field name="tblFeatureType" type="msls.application.tblFeatureType">
        /// Gets or sets the tblFeatureType for this tblFeature.
        /// </field>
        /// <field name="tblFeatureRelations" type="msls.EntityCollection" elementType="msls.application.tblFeatureRelation">
        /// Gets the tblFeatureRelations for this tblFeature.
        /// </field>
        /// <field name="CreatedBy" type="String">
        /// Gets or sets the createdBy for this tblFeature.
        /// </field>
        /// <field name="Created" type="Date">
        /// Gets or sets the created for this tblFeature.
        /// </field>
        /// <field name="ModifiedBy" type="String">
        /// Gets or sets the modifiedBy for this tblFeature.
        /// </field>
        /// <field name="Modified" type="Date">
        /// Gets or sets the modified for this tblFeature.
        /// </field>
        /// <field name="RowVersion" type="Array">
        /// Gets or sets the rowVersion for this tblFeature.
        /// </field>
        /// <field name="details" type="msls.application.tblFeature.Details">
        /// Gets the details for this tblFeature.
        /// </field>
        $Entity.call(this, entitySet);
    }

    function tblFeatureType(entitySet) {
        /// <summary>
        /// Represents the tblFeatureType entity type.
        /// </summary>
        /// <param name="entitySet" type="msls.EntitySet" optional="true">
        /// The entity set that should contain this tblFeatureType.
        /// </param>
        /// <field name="Id" type="Number">
        /// Gets or sets the id for this tblFeatureType.
        /// </field>
        /// <field name="type" type="String">
        /// Gets or sets the type for this tblFeatureType.
        /// </field>
        /// <field name="tblFeaturesCollection" type="msls.EntityCollection" elementType="msls.application.tblFeature">
        /// Gets the tblFeaturesCollection for this tblFeatureType.
        /// </field>
        /// <field name="CreatedBy" type="String">
        /// Gets or sets the createdBy for this tblFeatureType.
        /// </field>
        /// <field name="Created" type="Date">
        /// Gets or sets the created for this tblFeatureType.
        /// </field>
        /// <field name="ModifiedBy" type="String">
        /// Gets or sets the modifiedBy for this tblFeatureType.
        /// </field>
        /// <field name="Modified" type="Date">
        /// Gets or sets the modified for this tblFeatureType.
        /// </field>
        /// <field name="RowVersion" type="Array">
        /// Gets or sets the rowVersion for this tblFeatureType.
        /// </field>
        /// <field name="details" type="msls.application.tblFeatureType.Details">
        /// Gets the details for this tblFeatureType.
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

    function tblIp(entitySet) {
        /// <summary>
        /// Represents the tblIp entity type.
        /// </summary>
        /// <param name="entitySet" type="msls.EntitySet" optional="true">
        /// The entity set that should contain this tblIp.
        /// </param>
        /// <field name="Id" type="Number">
        /// Gets or sets the id for this tblIp.
        /// </field>
        /// <field name="ip" type="String">
        /// Gets or sets the ip for this tblIp.
        /// </field>
        /// <field name="tblComputer" type="msls.application.tblComputer">
        /// Gets or sets the tblComputer for this tblIp.
        /// </field>
        /// <field name="CreatedBy" type="String">
        /// Gets or sets the createdBy for this tblIp.
        /// </field>
        /// <field name="Created" type="Date">
        /// Gets or sets the created for this tblIp.
        /// </field>
        /// <field name="ModifiedBy" type="String">
        /// Gets or sets the modifiedBy for this tblIp.
        /// </field>
        /// <field name="Modified" type="Date">
        /// Gets or sets the modified for this tblIp.
        /// </field>
        /// <field name="RowVersion" type="Array">
        /// Gets or sets the rowVersion for this tblIp.
        /// </field>
        /// <field name="details" type="msls.application.tblIp.Details">
        /// Gets the details for this tblIp.
        /// </field>
        $Entity.call(this, entitySet);
    }

    function tblLokale(entitySet) {
        /// <summary>
        /// Represents the tblLokale entity type.
        /// </summary>
        /// <param name="entitySet" type="msls.EntitySet" optional="true">
        /// The entity set that should contain this tblLokale.
        /// </param>
        /// <field name="Id" type="Number">
        /// Gets or sets the id for this tblLokale.
        /// </field>
        /// <field name="lokale" type="String">
        /// Gets or sets the lokale for this tblLokale.
        /// </field>
        /// <field name="etage" type="String">
        /// Gets or sets the etage for this tblLokale.
        /// </field>
        /// <field name="note" type="String">
        /// Gets or sets the note for this tblLokale.
        /// </field>
        /// <field name="tblBrugers" type="msls.EntityCollection" elementType="msls.application.tblBruger">
        /// Gets the tblBrugers for this tblLokale.
        /// </field>
        /// <field name="tblComputers" type="msls.EntityCollection" elementType="msls.application.tblComputer">
        /// Gets the tblComputers for this tblLokale.
        /// </field>
        /// <field name="CreatedBy" type="String">
        /// Gets or sets the createdBy for this tblLokale.
        /// </field>
        /// <field name="Created" type="Date">
        /// Gets or sets the created for this tblLokale.
        /// </field>
        /// <field name="ModifiedBy" type="String">
        /// Gets or sets the modifiedBy for this tblLokale.
        /// </field>
        /// <field name="Modified" type="Date">
        /// Gets or sets the modified for this tblLokale.
        /// </field>
        /// <field name="RowVersion" type="Array">
        /// Gets or sets the rowVersion for this tblLokale.
        /// </field>
        /// <field name="details" type="msls.application.tblLokale.Details">
        /// Gets the details for this tblLokale.
        /// </field>
        $Entity.call(this, entitySet);
    }

    function tblBrugerRelation(entitySet) {
        /// <summary>
        /// Represents the tblBrugerRelation entity type.
        /// </summary>
        /// <param name="entitySet" type="msls.EntitySet" optional="true">
        /// The entity set that should contain this tblBrugerRelation.
        /// </param>
        /// <field name="Id" type="Number">
        /// Gets or sets the id for this tblBrugerRelation.
        /// </field>
        /// <field name="tblBruger" type="msls.application.tblBruger">
        /// Gets or sets the tblBruger for this tblBrugerRelation.
        /// </field>
        /// <field name="tblComputer" type="msls.application.tblComputer">
        /// Gets or sets the tblComputer for this tblBrugerRelation.
        /// </field>
        /// <field name="CreatedBy" type="String">
        /// Gets or sets the createdBy for this tblBrugerRelation.
        /// </field>
        /// <field name="Created" type="Date">
        /// Gets or sets the created for this tblBrugerRelation.
        /// </field>
        /// <field name="ModifiedBy" type="String">
        /// Gets or sets the modifiedBy for this tblBrugerRelation.
        /// </field>
        /// <field name="Modified" type="Date">
        /// Gets or sets the modified for this tblBrugerRelation.
        /// </field>
        /// <field name="RowVersion" type="Array">
        /// Gets or sets the rowVersion for this tblBrugerRelation.
        /// </field>
        /// <field name="details" type="msls.application.tblBrugerRelation.Details">
        /// Gets the details for this tblBrugerRelation.
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
        /// <field name="tblFeatureRelations" type="msls.EntitySet">
        /// Gets the tblFeatureRelations entity set.
        /// </field>
        /// <field name="tblFeatures" type="msls.EntitySet">
        /// Gets the tblFeatures entity set.
        /// </field>
        /// <field name="tblFeatureTypes" type="msls.EntitySet">
        /// Gets the tblFeatureTypes entity set.
        /// </field>
        /// <field name="tblHWs" type="msls.EntitySet">
        /// Gets the tblHWs entity set.
        /// </field>
        /// <field name="tblHWtypes" type="msls.EntitySet">
        /// Gets the tblHWtypes entity set.
        /// </field>
        /// <field name="tblIps" type="msls.EntitySet">
        /// Gets the tblIps entity set.
        /// </field>
        /// <field name="tblLokales" type="msls.EntitySet">
        /// Gets the tblLokales entity set.
        /// </field>
        /// <field name="tblBrugerRelations" type="msls.EntitySet">
        /// Gets the tblBrugerRelations entity set.
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
            { name: "tblLokale", kind: "reference", type: tblLokale },
            { name: "tblBrugerRelations", kind: "collection", elementType: tblBrugerRelation },
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
            { name: "tblFeatureRelations", kind: "collection", elementType: tblFeatureRelation },
            { name: "tblIps", kind: "collection", elementType: tblIp },
            { name: "tblBrugerRelations", kind: "collection", elementType: tblBrugerRelation },
            { name: "tblLokale", kind: "reference", type: tblLokale },
            { name: "CreatedBy", type: String, isReadOnly: true },
            { name: "Created", type: Date, isReadOnly: true },
            { name: "ModifiedBy", type: String, isReadOnly: true },
            { name: "Modified", type: Date, isReadOnly: true },
            { name: "RowVersion", type: Array }
        ]),

        tblFeatureRelation: $defineEntity(tblFeatureRelation, [
            { name: "Id", type: Number },
            { name: "tblFeature", kind: "reference", type: tblFeature },
            { name: "tblComputer", kind: "reference", type: tblComputer },
            { name: "CreatedBy", type: String, isReadOnly: true },
            { name: "Created", type: Date, isReadOnly: true },
            { name: "ModifiedBy", type: String, isReadOnly: true },
            { name: "Modified", type: Date, isReadOnly: true },
            { name: "RowVersion", type: Array }
        ]),

        tblFeature: $defineEntity(tblFeature, [
            { name: "Id", type: Number },
            { name: "navn", type: String },
            { name: "tblFeatureType", kind: "reference", type: tblFeatureType },
            { name: "tblFeatureRelations", kind: "collection", elementType: tblFeatureRelation },
            { name: "CreatedBy", type: String, isReadOnly: true },
            { name: "Created", type: Date, isReadOnly: true },
            { name: "ModifiedBy", type: String, isReadOnly: true },
            { name: "Modified", type: Date, isReadOnly: true },
            { name: "RowVersion", type: Array }
        ]),

        tblFeatureType: $defineEntity(tblFeatureType, [
            { name: "Id", type: Number },
            { name: "type", type: String },
            { name: "tblFeaturesCollection", kind: "collection", elementType: tblFeature },
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

        tblIp: $defineEntity(tblIp, [
            { name: "Id", type: Number },
            { name: "ip", type: String },
            { name: "tblComputer", kind: "reference", type: tblComputer },
            { name: "CreatedBy", type: String, isReadOnly: true },
            { name: "Created", type: Date, isReadOnly: true },
            { name: "ModifiedBy", type: String, isReadOnly: true },
            { name: "Modified", type: Date, isReadOnly: true },
            { name: "RowVersion", type: Array }
        ]),

        tblLokale: $defineEntity(tblLokale, [
            { name: "Id", type: Number },
            { name: "lokale", type: String },
            { name: "etage", type: String },
            { name: "note", type: String },
            { name: "tblBrugers", kind: "collection", elementType: tblBruger },
            { name: "tblComputers", kind: "collection", elementType: tblComputer },
            { name: "CreatedBy", type: String, isReadOnly: true },
            { name: "Created", type: Date, isReadOnly: true },
            { name: "ModifiedBy", type: String, isReadOnly: true },
            { name: "Modified", type: Date, isReadOnly: true },
            { name: "RowVersion", type: Array }
        ]),

        tblBrugerRelation: $defineEntity(tblBrugerRelation, [
            { name: "Id", type: Number },
            { name: "tblBruger", kind: "reference", type: tblBruger },
            { name: "tblComputer", kind: "reference", type: tblComputer },
            { name: "CreatedBy", type: String, isReadOnly: true },
            { name: "Created", type: Date, isReadOnly: true },
            { name: "ModifiedBy", type: String, isReadOnly: true },
            { name: "Modified", type: Date, isReadOnly: true },
            { name: "RowVersion", type: Array }
        ]),

        ApplicationData: $defineDataService(ApplicationData, lightSwitchApplication.rootUri + "/ApplicationData.svc", [
            { name: "tblBrugers", elementType: tblBruger },
            { name: "tblComputers", elementType: tblComputer },
            { name: "tblFeatureRelations", elementType: tblFeatureRelation },
            { name: "tblFeatures", elementType: tblFeature },
            { name: "tblFeatureTypes", elementType: tblFeatureType },
            { name: "tblHWs", elementType: tblHW },
            { name: "tblHWtypes", elementType: tblHWtype },
            { name: "tblIps", elementType: tblIp },
            { name: "tblLokales", elementType: tblLokale },
            { name: "tblBrugerRelations", elementType: tblBrugerRelation }
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
                name: "tblFeatureRelations_SingleOrDefault", value: function (Id) {
                    return new $DataServiceQuery({ _entitySet: this.tblFeatureRelations },
                        lightSwitchApplication.rootUri + "/ApplicationData.svc" + "/tblFeatureRelations(" + "Id=" + $toODataString(Id, "Int32?") + ")"
                    );
                }
            },
            {
                name: "tblFeatures_SingleOrDefault", value: function (Id) {
                    return new $DataServiceQuery({ _entitySet: this.tblFeatures },
                        lightSwitchApplication.rootUri + "/ApplicationData.svc" + "/tblFeatures(" + "Id=" + $toODataString(Id, "Int32?") + ")"
                    );
                }
            },
            {
                name: "tblFeatureTypes_SingleOrDefault", value: function (Id) {
                    return new $DataServiceQuery({ _entitySet: this.tblFeatureTypes },
                        lightSwitchApplication.rootUri + "/ApplicationData.svc" + "/tblFeatureTypes(" + "Id=" + $toODataString(Id, "Int32?") + ")"
                    );
                }
            },
            {
                name: "tblHWs_SingleOrDefault", value: function (Id) {
                    return new $DataServiceQuery({ _entitySet: this.tblHWs },
                        lightSwitchApplication.rootUri + "/ApplicationData.svc" + "/tblHWs(" + "Id=" + $toODataString(Id, "Int32?") + ")"
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
                name: "tblIps_SingleOrDefault", value: function (Id) {
                    return new $DataServiceQuery({ _entitySet: this.tblIps },
                        lightSwitchApplication.rootUri + "/ApplicationData.svc" + "/tblIps(" + "Id=" + $toODataString(Id, "Int32?") + ")"
                    );
                }
            },
            {
                name: "tblLokales_SingleOrDefault", value: function (Id) {
                    return new $DataServiceQuery({ _entitySet: this.tblLokales },
                        lightSwitchApplication.rootUri + "/ApplicationData.svc" + "/tblLokales(" + "Id=" + $toODataString(Id, "Int32?") + ")"
                    );
                }
            },
            {
                name: "tblBrugerRelations_SingleOrDefault", value: function (Id) {
                    return new $DataServiceQuery({ _entitySet: this.tblBrugerRelations },
                        lightSwitchApplication.rootUri + "/ApplicationData.svc" + "/tblBrugerRelations(" + "Id=" + $toODataString(Id, "Int32?") + ")"
                    );
                }
            }
        ]),

        DataWorkspace: $defineDataWorkspace(DataWorkspace, [
            { name: "ApplicationData", type: ApplicationData }
        ])

    });

}(msls.application));
