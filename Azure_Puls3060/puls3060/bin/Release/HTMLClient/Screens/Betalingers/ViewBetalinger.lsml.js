/// <reference path="~/GeneratedArtifacts/viewModel.js" />

myapp.ViewBetalinger.Details_postRender = function (element, contentItem) {
    // Write code here.
    var name = contentItem.screen.tblFikBetaling.details.getModel()[':@SummaryProperty'].property.name;
    contentItem.dataBind("screen.tblFikBetaling." + name, function (value) {
        contentItem.screen.details.displayName = value;
    });
}

