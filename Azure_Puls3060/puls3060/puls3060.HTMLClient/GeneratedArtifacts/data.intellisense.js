/// <reference path="data.js" />

(function (lightSwitchApplication) {

    msls._addEntryPoints(lightSwitchApplication.tblMedlem, {
        /// <field>
        /// Called when a new tblMedlem is created.
        /// <br/>created(msls.application.tblMedlem entity)
        /// </field>
        created: [lightSwitchApplication.tblMedlem]
    });

    msls._addEntryPoints(lightSwitchApplication.vMedmemLogText, {
        /// <field>
        /// Called when a new vMedmemLogText is created.
        /// <br/>created(msls.application.vMedmemLogText entity)
        /// </field>
        created: [lightSwitchApplication.vMedmemLogText]
    });

    msls._addEntryPoints(lightSwitchApplication.tblFikBetaling, {
        /// <field>
        /// Called when a new tblFikBetaling is created.
        /// <br/>created(msls.application.tblFikBetaling entity)
        /// </field>
        created: [lightSwitchApplication.tblFikBetaling]
    });

    msls._addEntryPoints(lightSwitchApplication.tblProjekt, {
        /// <field>
        /// Called when a new tblProjekt is created.
        /// <br/>created(msls.application.tblProjekt entity)
        /// </field>
        created: [lightSwitchApplication.tblProjekt]
    });

}(msls.application));
