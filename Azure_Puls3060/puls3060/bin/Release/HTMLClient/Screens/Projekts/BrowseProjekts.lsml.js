/// <reference path="~/GeneratedArtifacts/viewModel.js" />

myapp.BrowseProjekts.created = function (screen) {
    // Write code here.
    if (myapp.permissions["LightSwitchApplication:CanInsertProjekt"]) {
        screen.findContentItem("AddProjekt").isEnabled = true;
    }
    else {
        screen.findContentItem("AddProjekt").isEnabled = false;
    }
};