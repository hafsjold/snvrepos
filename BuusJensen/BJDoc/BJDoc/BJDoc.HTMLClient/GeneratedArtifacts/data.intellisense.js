/// <reference path="data.js" />

(function (lightSwitchApplication) {

    msls._addEntryPoints(lightSwitchApplication.tblBruger, {
        /// <field>
        /// Called when a new tblBruger is created.
        /// <br/>created(msls.application.tblBruger entity)
        /// </field>
        created: [lightSwitchApplication.tblBruger]
    });

    msls._addEntryPoints(lightSwitchApplication.tblComputer, {
        /// <field>
        /// Called when a new tblComputer is created.
        /// <br/>created(msls.application.tblComputer entity)
        /// </field>
        created: [lightSwitchApplication.tblComputer]
    });

    msls._addEntryPoints(lightSwitchApplication.tblFeatureRelation, {
        /// <field>
        /// Called when a new tblFeatureRelation is created.
        /// <br/>created(msls.application.tblFeatureRelation entity)
        /// </field>
        created: [lightSwitchApplication.tblFeatureRelation]
    });

    msls._addEntryPoints(lightSwitchApplication.tblFeature, {
        /// <field>
        /// Called when a new tblFeature is created.
        /// <br/>created(msls.application.tblFeature entity)
        /// </field>
        created: [lightSwitchApplication.tblFeature]
    });

    msls._addEntryPoints(lightSwitchApplication.tblFeatureType, {
        /// <field>
        /// Called when a new tblFeatureType is created.
        /// <br/>created(msls.application.tblFeatureType entity)
        /// </field>
        created: [lightSwitchApplication.tblFeatureType]
    });

    msls._addEntryPoints(lightSwitchApplication.tblHW, {
        /// <field>
        /// Called when a new tblHW is created.
        /// <br/>created(msls.application.tblHW entity)
        /// </field>
        created: [lightSwitchApplication.tblHW]
    });

    msls._addEntryPoints(lightSwitchApplication.tblHWtype, {
        /// <field>
        /// Called when a new tblHWtype is created.
        /// <br/>created(msls.application.tblHWtype entity)
        /// </field>
        created: [lightSwitchApplication.tblHWtype]
    });

    msls._addEntryPoints(lightSwitchApplication.tblIp, {
        /// <field>
        /// Called when a new tblIp is created.
        /// <br/>created(msls.application.tblIp entity)
        /// </field>
        created: [lightSwitchApplication.tblIp]
    });

    msls._addEntryPoints(lightSwitchApplication.tblLokale, {
        /// <field>
        /// Called when a new tblLokale is created.
        /// <br/>created(msls.application.tblLokale entity)
        /// </field>
        created: [lightSwitchApplication.tblLokale]
    });

    msls._addEntryPoints(lightSwitchApplication.tblBrugerRelation, {
        /// <field>
        /// Called when a new tblBrugerRelation is created.
        /// <br/>created(msls.application.tblBrugerRelation entity)
        /// </field>
        created: [lightSwitchApplication.tblBrugerRelation]
    });

}(msls.application));
