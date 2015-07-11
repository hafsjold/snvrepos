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

    function tblMedlem(entitySet) {
        /// <summary>
        /// Represents the tblMedlem entity type.
        /// </summary>
        /// <param name="entitySet" type="msls.EntitySet" optional="true">
        /// The entity set that should contain this tblMedlem.
        /// </param>
        /// <field name="Navn" type="String">
        /// Gets or sets the navn for this tblMedlem.
        /// </field>
        /// <field name="Kaldenavn" type="String">
        /// Gets or sets the kaldenavn for this tblMedlem.
        /// </field>
        /// <field name="Adresse" type="String">
        /// Gets or sets the adresse for this tblMedlem.
        /// </field>
        /// <field name="Postnr" type="String">
        /// Gets or sets the postnr for this tblMedlem.
        /// </field>
        /// <field name="Bynavn" type="String">
        /// Gets or sets the bynavn for this tblMedlem.
        /// </field>
        /// <field name="Telefon" type="String">
        /// Gets or sets the telefon for this tblMedlem.
        /// </field>
        /// <field name="Email" type="String">
        /// Gets or sets the email for this tblMedlem.
        /// </field>
        /// <field name="Status" type="Number">
        /// Gets or sets the status for this tblMedlem.
        /// </field>
        /// <field name="Nr" type="Number">
        /// Gets or sets the nr for this tblMedlem.
        /// </field>
        /// <field name="Kon" type="String">
        /// Gets or sets the kon for this tblMedlem.
        /// </field>
        /// <field name="FodtDato" type="Date">
        /// Gets or sets the fodtDato for this tblMedlem.
        /// </field>
        /// <field name="Bank" type="String">
        /// Gets or sets the bank for this tblMedlem.
        /// </field>
        /// <field name="tblFikBetalings" type="msls.EntityCollection" elementType="msls.application.tblFikBetaling">
        /// Gets the tblFikBetalings for this tblMedlem.
        /// </field>
        /// <field name="vMedmemLogTexts" type="msls.EntityCollection" elementType="msls.application.vMedmemLogText">
        /// Gets the vMedmemLogTexts for this tblMedlem.
        /// </field>
        /// <field name="details" type="msls.application.tblMedlem.Details">
        /// Gets the details for this tblMedlem.
        /// </field>
        $Entity.call(this, entitySet);
    }

    function vMedmemLogText(entitySet) {
        /// <summary>
        /// Represents the vMedmemLogText entity type.
        /// </summary>
        /// <param name="entitySet" type="msls.EntitySet" optional="true">
        /// The entity set that should contain this vMedmemLogText.
        /// </param>
        /// <field name="id" type="Number">
        /// Gets or sets the id for this vMedmemLogText.
        /// </field>
        /// <field name="Nr" type="Number">
        /// Gets or sets the nr for this vMedmemLogText.
        /// </field>
        /// <field name="logdato" type="Date">
        /// Gets or sets the logdato for this vMedmemLogText.
        /// </field>
        /// <field name="Aktivitet" type="String">
        /// Gets or sets the aktivitet for this vMedmemLogText.
        /// </field>
        /// <field name="Dato" type="Date">
        /// Gets or sets the dato for this vMedmemLogText.
        /// </field>
        /// <field name="tblMedlem" type="msls.application.tblMedlem">
        /// Gets or sets the tblMedlem for this vMedmemLogText.
        /// </field>
        /// <field name="details" type="msls.application.vMedmemLogText.Details">
        /// Gets the details for this vMedmemLogText.
        /// </field>
        $Entity.call(this, entitySet);
    }

    function tblFikBetaling(entitySet) {
        /// <summary>
        /// Represents the tblFikBetaling entity type.
        /// </summary>
        /// <param name="entitySet" type="msls.EntitySet" optional="true">
        /// The entity set that should contain this tblFikBetaling.
        /// </param>
        /// <field name="Id" type="Number">
        /// Gets or sets the id for this tblFikBetaling.
        /// </field>
        /// <field name="tblProjekt" type="msls.application.tblProjekt">
        /// Gets or sets the tblProjekt for this tblFikBetaling.
        /// </field>
        /// <field name="tblMedlem" type="tblMedlem">
        /// Gets or sets the tblMedlem for this tblFikBetaling.
        /// </field>
        /// <field name="Belob" type="String">
        /// Gets or sets the belob for this tblFikBetaling.
        /// </field>
        /// <field name="BetalingsDato" type="Date">
        /// Gets or sets the betalingsDato for this tblFikBetaling.
        /// </field>
        /// <field name="FIKnr" type="String">
        /// Gets or sets the fIKnr for this tblFikBetaling.
        /// </field>
        /// <field name="tblMedlem_Nr" type="Number">
        /// Gets or sets the tblMedlem_Nr for this tblFikBetaling.
        /// </field>
        /// <field name="CreatedBy" type="String">
        /// Gets or sets the createdBy for this tblFikBetaling.
        /// </field>
        /// <field name="Created" type="Date">
        /// Gets or sets the created for this tblFikBetaling.
        /// </field>
        /// <field name="ModifiedBy" type="String">
        /// Gets or sets the modifiedBy for this tblFikBetaling.
        /// </field>
        /// <field name="Modified" type="Date">
        /// Gets or sets the modified for this tblFikBetaling.
        /// </field>
        /// <field name="RowVersion" type="Array">
        /// Gets or sets the rowVersion for this tblFikBetaling.
        /// </field>
        /// <field name="details" type="msls.application.tblFikBetaling.Details">
        /// Gets the details for this tblFikBetaling.
        /// </field>
        $Entity.call(this, entitySet);
    }

    function tblProjekt(entitySet) {
        /// <summary>
        /// Represents the tblProjekt entity type.
        /// </summary>
        /// <param name="entitySet" type="msls.EntitySet" optional="true">
        /// The entity set that should contain this tblProjekt.
        /// </param>
        /// <field name="Navn" type="String">
        /// Gets or sets the navn for this tblProjekt.
        /// </field>
        /// <field name="Projektnr" type="Number">
        /// Gets or sets the projektnr for this tblProjekt.
        /// </field>
        /// <field name="Id" type="Number">
        /// Gets or sets the id for this tblProjekt.
        /// </field>
        /// <field name="tblFikBetalings" type="msls.EntityCollection" elementType="msls.application.tblFikBetaling">
        /// Gets the tblFikBetalings for this tblProjekt.
        /// </field>
        /// <field name="CreatedBy" type="String">
        /// Gets or sets the createdBy for this tblProjekt.
        /// </field>
        /// <field name="Created" type="Date">
        /// Gets or sets the created for this tblProjekt.
        /// </field>
        /// <field name="ModifiedBy" type="String">
        /// Gets or sets the modifiedBy for this tblProjekt.
        /// </field>
        /// <field name="Modified" type="Date">
        /// Gets or sets the modified for this tblProjekt.
        /// </field>
        /// <field name="RowVersion" type="Array">
        /// Gets or sets the rowVersion for this tblProjekt.
        /// </field>
        /// <field name="details" type="msls.application.tblProjekt.Details">
        /// Gets the details for this tblProjekt.
        /// </field>
        $Entity.call(this, entitySet);
    }

    function dbPuls3060MedlemData(dataWorkspace) {
        /// <summary>
        /// Represents the dbPuls3060MedlemData data service.
        /// </summary>
        /// <param name="dataWorkspace" type="msls.DataWorkspace">
        /// The data workspace that created this data service.
        /// </param>
        /// <field name="tblMedlems" type="msls.EntitySet">
        /// Gets the tblMedlems entity set.
        /// </field>
        /// <field name="vMedmemLogTexts" type="msls.EntitySet">
        /// Gets the vMedmemLogTexts entity set.
        /// </field>
        /// <field name="details" type="msls.application.dbPuls3060MedlemData.Details">
        /// Gets the details for this data service.
        /// </field>
        $DataService.call(this, dataWorkspace);
    };

    function ApplicationData(dataWorkspace) {
        /// <summary>
        /// Represents the ApplicationData data service.
        /// </summary>
        /// <param name="dataWorkspace" type="msls.DataWorkspace">
        /// The data workspace that created this data service.
        /// </param>
        /// <field name="tblFikBetalings" type="msls.EntitySet">
        /// Gets the tblFikBetalings entity set.
        /// </field>
        /// <field name="tblProjekts" type="msls.EntitySet">
        /// Gets the tblProjekts entity set.
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
        /// <field name="dbPuls3060MedlemData" type="msls.application.dbPuls3060MedlemData">
        /// Gets the dbPuls3060MedlemData data service.
        /// </field>
        /// <field name="ApplicationData" type="msls.application.ApplicationData">
        /// Gets the ApplicationData data service.
        /// </field>
        /// <field name="details" type="msls.application.DataWorkspace.Details">
        /// Gets the details for this data workspace.
        /// </field>
        $DataWorkspace.call(this);
    };

    msls._addToNamespace("msls.application", {

        tblMedlem: $defineEntity(tblMedlem, [
            { name: "Navn", type: String },
            { name: "Kaldenavn", type: String },
            { name: "Adresse", type: String },
            { name: "Postnr", type: String },
            { name: "Bynavn", type: String },
            { name: "Telefon", type: String },
            { name: "Email", type: String },
            { name: "Status", type: Number },
            { name: "Nr", type: Number },
            { name: "Kon", type: String },
            { name: "FodtDato", type: Date },
            { name: "Bank", type: String },
            { name: "tblFikBetalings", kind: "virtualCollection", elementType: tblFikBetaling },
            { name: "vMedmemLogTexts", kind: "collection", elementType: vMedmemLogText }
        ]),

        vMedmemLogText: $defineEntity(vMedmemLogText, [
            { name: "id", type: Number },
            { name: "Nr", type: Number },
            { name: "logdato", type: Date },
            { name: "Aktivitet", type: String },
            { name: "Dato", type: Date },
            { name: "tblMedlem", kind: "reference", type: tblMedlem }
        ]),

        tblFikBetaling: $defineEntity(tblFikBetaling, [
            { name: "Id", type: Number },
            { name: "tblProjekt", kind: "reference", type: tblProjekt },
            { name: "tblMedlem", kind: "virtualReference", type: tblMedlem },
            { name: "Belob", type: String },
            { name: "BetalingsDato", type: Date },
            { name: "FIKnr", type: String },
            { name: "tblMedlem_Nr", type: Number },
            { name: "CreatedBy", type: String, isReadOnly: true },
            { name: "Created", type: Date, isReadOnly: true },
            { name: "ModifiedBy", type: String, isReadOnly: true },
            { name: "Modified", type: Date, isReadOnly: true },
            { name: "RowVersion", type: Array }
        ]),

        tblProjekt: $defineEntity(tblProjekt, [
            { name: "Navn", type: String },
            { name: "Projektnr", type: Number },
            { name: "Id", type: Number },
            { name: "tblFikBetalings", kind: "collection", elementType: tblFikBetaling },
            { name: "CreatedBy", type: String, isReadOnly: true },
            { name: "Created", type: Date, isReadOnly: true },
            { name: "ModifiedBy", type: String, isReadOnly: true },
            { name: "Modified", type: Date, isReadOnly: true },
            { name: "RowVersion", type: Array }
        ]),

        dbPuls3060MedlemData: $defineDataService(dbPuls3060MedlemData, lightSwitchApplication.rootUri + "/dbPuls3060MedlemData.svc", [
            { name: "tblMedlems", elementType: tblMedlem },
            { name: "vMedmemLogTexts", elementType: vMedmemLogText }
        ], [
            {
                name: "tblMedlems_SingleOrDefault", value: function (Nr) {
                    return new $DataServiceQuery({ _entitySet: this.tblMedlems },
                        lightSwitchApplication.rootUri + "/dbPuls3060MedlemData.svc" + "/tblMedlems(" + "Nr=" + $toODataString(Nr, "Int32?") + ")"
                    );
                }
            },
            {
                name: "vMedmemLogTexts_SingleOrDefault", value: function (id) {
                    return new $DataServiceQuery({ _entitySet: this.vMedmemLogTexts },
                        lightSwitchApplication.rootUri + "/dbPuls3060MedlemData.svc" + "/vMedmemLogTexts(" + "id=" + $toODataString(id, "Int32?") + ")"
                    );
                }
            }
        ]),

        ApplicationData: $defineDataService(ApplicationData, lightSwitchApplication.rootUri + "/ApplicationData.svc", [
            { name: "tblFikBetalings", elementType: tblFikBetaling },
            { name: "tblProjekts", elementType: tblProjekt }
        ], [
            {
                name: "tblFikBetalings_SingleOrDefault", value: function (Id) {
                    return new $DataServiceQuery({ _entitySet: this.tblFikBetalings },
                        lightSwitchApplication.rootUri + "/ApplicationData.svc" + "/tblFikBetalings(" + "Id=" + $toODataString(Id, "Int32?") + ")"
                    );
                }
            },
            {
                name: "tblProjekts_SingleOrDefault", value: function (Id) {
                    return new $DataServiceQuery({ _entitySet: this.tblProjekts },
                        lightSwitchApplication.rootUri + "/ApplicationData.svc" + "/tblProjekts(" + "Id=" + $toODataString(Id, "Int32?") + ")"
                    );
                }
            }
        ]),

        DataWorkspace: $defineDataWorkspace(DataWorkspace, [
            { name: "dbPuls3060MedlemData", type: dbPuls3060MedlemData },
            { name: "ApplicationData", type: ApplicationData }
        ])

    });

}(msls.application));
