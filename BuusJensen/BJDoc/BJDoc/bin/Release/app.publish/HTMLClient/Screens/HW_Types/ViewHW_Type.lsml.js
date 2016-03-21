/// <reference path="~/GeneratedArtifacts/viewModel.js" />

myapp.ViewHW_Type.Details_postRender = function (element, contentItem) {
    // Write code here.
    var name = contentItem.screen.tblHWtype.details.getModel()[':@SummaryProperty'].property.name;
    contentItem.dataBind("screen.tblHWtype." + name, function (value) {
        contentItem.screen.details.displayName = value;
    });
}

