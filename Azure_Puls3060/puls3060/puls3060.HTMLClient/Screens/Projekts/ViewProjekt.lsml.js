/// <reference path="~/GeneratedArtifacts/viewModel.js" />

myapp.ViewProjekt.Details_postRender = function (element, contentItem) {
    // Write code here.
    var name = contentItem.screen.tblProjekt.details.getModel()[':@SummaryProperty'].property.name;
    contentItem.dataBind("screen.tblProjekt." + name, function (value) {
        contentItem.screen.details.displayName = value;
    });
}

