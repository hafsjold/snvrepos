/// <reference path="~/GeneratedArtifacts/viewModel.js" />

myapp.ViewComputer.Details_postRender = function (element, contentItem) {
    // Write code here.
    var name = contentItem.screen.tblComputer.details.getModel()[':@SummaryProperty'].property.name;
    contentItem.dataBind("screen.tblComputer." + name, function (value) {
        contentItem.screen.details.displayName = value;
    });
}

