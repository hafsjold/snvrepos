/// <reference path="~/GeneratedArtifacts/viewModel.js" />

myapp.ViewBruger.Details_postRender = function (element, contentItem) {
    // Write code here.
    var name = contentItem.screen.tblBruger.details.getModel()[':@SummaryProperty'].property.name;
    contentItem.dataBind("screen.tblBruger." + name, function (value) {
        contentItem.screen.details.displayName = value;
    });
}

