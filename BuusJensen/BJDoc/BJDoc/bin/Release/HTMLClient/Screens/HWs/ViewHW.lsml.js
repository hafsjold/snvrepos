/// <reference path="~/GeneratedArtifacts/viewModel.js" />

myapp.ViewHW.Details_postRender = function (element, contentItem) {
    // Write code here.
    var name = contentItem.screen.tblHW.details.getModel()[':@SummaryProperty'].property.name;
    contentItem.dataBind("screen.tblHW." + name, function (value) {
        contentItem.screen.details.displayName = value;
    });
}

