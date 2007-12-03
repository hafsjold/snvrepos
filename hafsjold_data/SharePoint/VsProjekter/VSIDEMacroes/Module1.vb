Imports System
Imports EnvDTE
Imports EnvDTE80
Imports System.Diagnostics
Imports System.Data
Imports System.Text.RegularExpressions
Imports System.Xml
Imports System.Xml.Schema
Imports Microsoft.SharePoint
Imports wssGetList

Public Module Module1
    Sub InsertGUID()
        Dim textSelection As EnvDTE.TextSelection
        Dim MyGuid As System.Guid = System.Guid.NewGuid()

        textSelection = CType(DTE.ActiveDocument.Selection(), EnvDTE.TextSelection)
        textSelection.Text = MyGuid.ToString ''System.DateTime.Now.ToLongTimeString()
    End Sub
    Sub _spmha()

        Dim textSelection As EnvDTE.TextSelection
        textSelection = CType(DTE.ActiveDocument.Selection(), EnvDTE.TextSelection)

        Dim myList As wssGetList.getList = New wssGetList.getList("http://hd16.hafsjold.dk", "administrator", "m733", "hd16")
        Dim dsItems As System.Data.DataSet = myList.getListData("ProPurList")
        Dim rows As DataTable = dsItems.Tables("row")

        For Each row As DataRow In rows.Rows

            Dim Id As Integer = CType(row("ows_ID"), Integer)
            Dim SysName As String = CType(row("ows_Title"), String)
            Dim FieldName As String = CType(row("ows_FieldName"), String)
            Dim DisplayNameDK As String = CType(row("ows_DisplayNameDK"), String)
            Dim DisplayNameUK As String = CType(row("ows_DisplayNameUK"), String)
            Dim Guid As String = CType(row("ows_GUID0"), String)
            Dim KolonneType As String = CType(row("ows_KolonneType"), String)


        Next row

        'textSelection.Text = FieldName

    End Sub

    Sub _mhaXML()
        'Dim SysName As String = "Alice"
        'Dim DisplayNameDK As String = "Alice value DK"
        'Dim DisplayNameUK As String = "Alice value UK"
        'Dim FieldName As String = "Alice comment"

        Const COREFILE = "Resource1"
        Const FILENAME_FIELDS = "fields.xml"
        Const FILENAME_DK = "Resource1" & ".da-dk.resx"
        Const FILENAME_UK = "Resource1" & ".resx"

        Dim XMLFileFIELDS As TextDocument = DTE.Documents.Item(FILENAME_FIELDS).Object("TextDocument")
        Dim XMLFileFIELDStextSel As TextSelection = XMLFileFIELDS.Selection
        XMLFileFIELDStextSel.SelectAll()
        Dim docFIELDS As System.Xml.XmlDocument = New System.Xml.XmlDocument()
        docFIELDS.LoadXml(XMLFileFIELDStextSel.Text)
        XMLFileFIELDStextSel = Nothing

        Dim nsmgr As New XmlNamespaceManager(docFIELDS.NameTable)
        nsmgr.AddNamespace("", "file:///C:\Program Files\Common Files\Microsoft Shared\web server extensions\12\TEMPLATE\XML\wss.xsd")
        ''nsmgr.AddNamespace("", "http://schemas.microsoft.com/sharepoint/")

        'Dim schema As New XmlSchema()
        'schema.TargetNamespace = "http://schemas.microsoft.com/sharepoint/"
        'schema.ElementFormDefault = XmlSchemaForm.Qualified
        'schema.AttributeFormDefault = XmlSchemaForm.Unqualified
        'schema.Version = "1.0"
        'schema.Namespaces.Add("xmlns", "http://schemas.microsoft.com/sharepoint/")
        'schema.Namespaces.Add("mstns", "http://schemas.microsoft.com/sharepoint/")


        'Dim wss As New XmlSchemaInclude
        'wss.SchemaLocation = "C:\Program Files\Common Files\Microsoft Shared\web server extensions\12\TEMPLATE\XML\wss.xsd"
        'schema.Includes.Add(wss)

        'Dim nsmgr As New XmlNamespaceManager(docFIELDS.NameTable)
        'Dim st As New System.IO.FileStream("c:\mha.xsd", IO.FileMode.OpenOrCreate)
        'Dim sw As New IO.StreamWriter(st)
        'schema.Write(sw)

        'Dim sw2 As New IO.StringWriter
        'schema.Write(sw2, nsmgr)


        'Dim elements As System.Xml.XmlNode = docFIELDS.SelectSingleNode("//root/Elements", nsmgr)


        Dim XMLFileDK As TextDocument = DTE.Documents.Item(FILENAME_DK).Object("TextDocument")
        Dim XMLFileDKtextSel As TextSelection = XMLFileDK.Selection
        XMLFileDKtextSel.SelectAll()
        Dim docDK As System.Xml.XmlDocument = New System.Xml.XmlDocument()
        docDK.LoadXml(XMLFileDKtextSel.Text)
        XMLFileDKtextSel = Nothing


        Dim XMLFileUK As TextDocument = DTE.Documents.Item(FILENAME_UK).Object("TextDocument")
        Dim XMLFileUKtextSel As TextSelection = XMLFileUK.Selection
        XMLFileUKtextSel.SelectAll()
        Dim docUK As System.Xml.XmlDocument = New System.Xml.XmlDocument()
        docUK.LoadXml(XMLFileUKtextSel.Text)
        XMLFileUKtextSel = Nothing

        Dim myList As wssGetList.getList = New wssGetList.getList("http://hd16.hafsjold.dk", "administrator", "m733", "hd16")
        Dim dsItems As System.Data.DataSet = myList.getListData("ProPurList")
        Dim rows As DataTable = dsItems.Tables("row")

        For Each row As DataRow In rows.Rows

            Dim Id As Integer = CType(row("ows_ID"), Integer)
            Dim SysName As String = CType(row("ows_Title"), String)
            Dim FieldName As String = CType(row("ows_FieldName"), String)
            Dim DisplayNameDK As String = CType(row("ows_DisplayNameDK"), String)
            Dim DisplayNameUK As String = CType(row("ows_DisplayNameUK"), String)
            Dim Guid As String = CType(row("ows_GUID0"), String)
            Dim KolonneType As String = CType(row("ows_KolonneType"), String)

            Dim dataFIELDS As System.Xml.XmlElement = createFieldElement(docFIELDS, Guid, SysName, COREFILE, KolonneType)
            Dim dataDK As System.Xml.XmlElement = createDataElement(docDK, SysName, DisplayNameDK, FieldName)
            Dim dataUK As System.Xml.XmlElement = createDataElement(docUK, SysName, DisplayNameUK, FieldName)
        Next row

        Dim starteditPntFIELDS As EditPoint = XMLFileFIELDS.StartPoint.CreateEditPoint()
        Dim endeditPntFIELDS As EditPoint = XMLFileFIELDS.StartPoint.CreateEditPoint
        starteditPntFIELDS.StartOfDocument()
        endeditPntFIELDS.EndOfDocument()
        starteditPntFIELDS.Delete(endeditPntFIELDS)
        starteditPntFIELDS.Insert(docFIELDS.OuterXml)

        Dim starteditPntDK As EditPoint = XMLFileDK.StartPoint.CreateEditPoint()
        Dim endeditPntDK As EditPoint = XMLFileDK.StartPoint.CreateEditPoint
        starteditPntDK.StartOfDocument()
        endeditPntDK.EndOfDocument()
        starteditPntDK.Delete(endeditPntDK)
        starteditPntDK.Insert(docDK.OuterXml)

        Dim starteditPntUK As EditPoint = XMLFileUK.StartPoint.CreateEditPoint()
        Dim endeditPntUK As EditPoint = XMLFileUK.StartPoint.CreateEditPoint
        starteditPntUK.StartOfDocument()
        endeditPntUK.EndOfDocument()
        starteditPntUK.Delete(endeditPntUK)
        starteditPntUK.Insert(docUK.OuterXml)

    End Sub

    Sub GenerateFields()
        Const COREFILE = "ProvPur"
        Const FILENAME_FIELDS = "fields.xml"
        Const FILENAME_DK = COREFILE & ".da-dk.resx"
        Const FILENAME_UK = COREFILE & ".resx"

        Dim XMLFileFIELDS As TextDocument = DTE.Documents.Item(FILENAME_FIELDS).Object("TextDocument")
        Dim XMLFileFIELDStextSel As TextSelection = XMLFileFIELDS.Selection
        XMLFileFIELDStextSel.SelectAll()
        Dim strXML As String = Regex.Replace(XMLFileFIELDStextSel.Text, "xmlns=""[^""]*""", "")
        Dim docFIELDS As System.Xml.XmlDocument = New System.Xml.XmlDocument()
        docFIELDS.LoadXml(strXML)
        XMLFileFIELDStextSel = Nothing


        Dim XMLFileDK As TextDocument = DTE.Documents.Item(FILENAME_DK).Object("TextDocument")
        Dim XMLFileDKtextSel As TextSelection = XMLFileDK.Selection
        XMLFileDKtextSel.SelectAll()
        Dim docDK As System.Xml.XmlDocument = New System.Xml.XmlDocument()
        docDK.LoadXml(XMLFileDKtextSel.Text)
        XMLFileDKtextSel = Nothing


        Dim XMLFileUK As TextDocument = DTE.Documents.Item(FILENAME_UK).Object("TextDocument")
        Dim XMLFileUKtextSel As TextSelection = XMLFileUK.Selection
        XMLFileUKtextSel.SelectAll()
        Dim docUK As System.Xml.XmlDocument = New System.Xml.XmlDocument()
        docUK.LoadXml(XMLFileUKtextSel.Text)
        XMLFileUKtextSel = Nothing

        Dim myList As wssGetList.getList = New wssGetList.getList("http://hd16.hafsjold.dk", "administrator", "m733", "hd16")
        Dim dsItems As System.Data.DataSet = myList.getListData("ProPurList")
        Dim rows As DataTable = dsItems.Tables("row")

        For Each row As DataRow In rows.Rows

            Dim Id As Integer = CType(row("ows_ID"), Integer)
            Dim SysName As String = CType(row("ows_Title"), String)
            Dim KolonneType As String = CType(row("ows_KolonneType"), String)
            Dim DisplayNameDK As String = CType(row("ows_DisplayNameDK"), String)
            Dim DisplayNameUK As String = CType(row("ows_DisplayNameUK"), String)
            Dim FieldComment As String = CType(row("ows_FieldName"), String)
            Dim Guid As String = CType(row("ows_GUID0"), String)

            Dim dataFIELDS As System.Xml.XmlElement = createFieldElement(docFIELDS, Guid, SysName, COREFILE, KolonneType)
            Dim dataDK As System.Xml.XmlElement = createDataElement(docDK, SysName, DisplayNameDK, FieldComment)
            Dim dataUK As System.Xml.XmlElement = createDataElement(docUK, SysName, DisplayNameUK, FieldComment)
        Next row

        strXML = Regex.Replace(docFIELDS.OuterXml, "<Elements>", "<Elements xmlns=""http://schemas.microsoft.com/sharepoint/"">")

        Dim starteditPntFIELDS As EditPoint = XMLFileFIELDS.StartPoint.CreateEditPoint()
        Dim endeditPntFIELDS As EditPoint = XMLFileFIELDS.StartPoint.CreateEditPoint
        starteditPntFIELDS.StartOfDocument()
        endeditPntFIELDS.EndOfDocument()
        starteditPntFIELDS.Delete(endeditPntFIELDS)
        starteditPntFIELDS.Insert(strXML)

        Dim starteditPntDK As EditPoint = XMLFileDK.StartPoint.CreateEditPoint()
        Dim endeditPntDK As EditPoint = XMLFileDK.StartPoint.CreateEditPoint
        starteditPntDK.StartOfDocument()
        endeditPntDK.EndOfDocument()
        starteditPntDK.Delete(endeditPntDK)
        starteditPntDK.Insert(docDK.OuterXml)

        Dim starteditPntUK As EditPoint = XMLFileUK.StartPoint.CreateEditPoint()
        Dim endeditPntUK As EditPoint = XMLFileUK.StartPoint.CreateEditPoint
        starteditPntUK.StartOfDocument()
        endeditPntUK.EndOfDocument()
        starteditPntUK.Delete(endeditPntUK)
        starteditPntUK.Insert(docUK.OuterXml)

    End Sub

    Private Function createFieldElement( _
            ByRef pdoc As System.Xml.XmlDocument, _
            ByVal pid As String, _
            ByVal pname As String, _
            ByVal pcore As String, _
            ByVal KolonneType As String _
        ) As System.Xml.XmlElement

        Dim field As System.Xml.XmlElement = pdoc.CreateElement("Field")
        field.SetAttribute("ID", pid)
        field.SetAttribute("Name", pname)
        field.SetAttribute("DisplayName", "$Resources:" & pcore & "," & pname & ";")

        Select Case KolonneType
            Case "Text"
                field.SetAttribute("Type", "Text")
                field.SetAttribute("MaxLength", "255")

            Case "Note"
                field.SetAttribute("Type", "Note")
                field.SetAttribute("NumLines", "3")
                field.SetAttribute("RichText", "TRUE")

            Case "Choice"
                field.SetAttribute("Type", "Choice")

            Case "Number"
                field.SetAttribute("Type", "Number")
                field.SetAttribute("Decimals", "0")

            Case "Percentage"
                field.SetAttribute("Type", "Number")
                field.SetAttribute("Percentage", "TRUE")
                field.SetAttribute("Min", "0")
                field.SetAttribute("Max", "1")

            Case "Currency"
                field.SetAttribute("Type", "Currency")
                field.SetAttribute("Decimals", "2")

            Case "DateOnly"
                field.SetAttribute("Type", "DateTime")
                field.SetAttribute("Format", "DateOnly")

            Case "DateTime"
                field.SetAttribute("Type", "DateTime")

            Case "Boolean"
                field.SetAttribute("Type", "Boolean")

            Case Else

        End Select

        field.SetAttribute("Group", "$Resources:" & pcore & ",FieldsGroupName;")

        Dim elements As System.Xml.XmlNode = pdoc.SelectSingleNode("//Elements")
        Dim filter As String = "//Field[@ID=""" & pid & """]"
        Dim old_field As System.Xml.XmlNode = elements.SelectSingleNode(filter)

        If old_field Is Nothing Then
            elements.AppendChild(field)
        Else
            elements.ReplaceChild(field, old_field)
        End If

        Return field
    End Function

    Private Function createDataElement( _
            ByRef pdoc As System.Xml.XmlDocument, _
            ByVal pname As String, _
            ByVal pvalue As String, _
            Optional ByVal pcomment As String = Nothing _
        ) As System.Xml.XmlElement

        Dim data As System.Xml.XmlElement = pdoc.CreateElement("data")
        data.SetAttribute("name", pname)
        data.SetAttribute("xml:space", "preserve")

        Dim value As System.Xml.XmlElement = pdoc.CreateElement("value")
        value.InnerText = pvalue
        data.AppendChild(value)

        If Not pcomment = Nothing Then
            Dim comment As System.Xml.XmlElement = pdoc.CreateElement("comment")
            comment.InnerText = pcomment
            data.AppendChild(comment)
        End If

        Dim root As System.Xml.XmlNode = pdoc.SelectSingleNode("//root")
        Dim old_data As System.Xml.XmlNode = root.SelectSingleNode("//data[@name='" & pname & "']")
        If old_data Is Nothing Then
            root.AppendChild(data)
        Else
            root.ReplaceChild(data, old_data)
        End If

        Return data
    End Function

    Sub _mha()
        Dim ModleFile As TextDocument = DTE.Documents.Item("ModleFile.txt").Object("TextDocument")
        Dim TextFile1 As TextDocument = DTE.Documents.Item("TextFile1.txt").Object("TextDocument")

        Dim ModleFiletextSel As TextSelection = ModleFile.Selection
        Dim TextFile1editPnt As EditPoint = TextFile1.StartPoint.CreateEditPoint()

        ' Position insertion point at beginning of "Line 2".
        ModleFiletextSel.StartOfDocument()
        ModleFiletextSel.LineDown(False, 2)
        ModleFiletextSel.LineDown(True, 1)
        Dim source As String = ModleFiletextSel.Text.ToString()
        ModleFiletextSel.Copy()

        TextFile1editPnt.StartOfDocument()
        TextFile1editPnt.LineDown(5)
        TextFile1editPnt.Paste()
    End Sub

    Sub ReplacePatternExample()
        Dim objTextDoc As TextDocument
        Dim objTxtPt As TextPoint
        Dim iCtr As Integer
        Dim strAnswer As String

        ' Create a new text file.
        DTE.ItemOperations.NewFile("General\Text File")

        ' Set object references, create an EditPoint.
        Dim objSel As TextSelection = DTE.ActiveDocument.Selection
        objTextDoc = DTE.ActiveDocument.Object("TextDocument")
        objTxtPt = objTextDoc.StartPoint.CreateEditPoint

        ' Insert ten lines of text.
        For iCtr = 1 To 10
            objTxtPt.Insert("This is a test." & Chr(13))
        Next iCtr

        strAnswer = MsgBox("Ready to replace 'test' with 'done deal'?", vbYesNo + vbQuestion)

        ' Looks for a specified word pattern and then replaces it with 
        ' another.
        If strAnswer = vbYes Then
            objSel.SelectAll()
            objSel.ReplacePattern("test", "done deal", EnvDTE.vsFindOptions.vsFindOptionsRegularExpression, )
        Else
            MsgBox("Okay, no replacements made.")
        End If
    End Sub

    Sub CreateExample()
        ' Open a solution before running this example.
        'Dim obj As Document
        'obj = DTE.ActiveDocument.Object("Document")

        'Dim obj2 As TextDocument
        'obj2 = DTE.ActiveDocument.Object("TextDocument")

        'Dim obj3 As TextDocument
        'obj3 = obj.Object("TextDocument")

        'Dim obj4 As TextDocument = CType(obj.Object(), TextDocument)



        Dim docs As Documents
        Dim soln As Solution
        Dim proj As Project
        Dim pname As String
        Dim pitem As ProjectItem
        Dim iname As String
        Dim dname As String
        Dim doc As Document
        Dim doc2 As TextDocument

        docs = DTE.Documents
        doc = DTE.Documents.Item("TextFile1.txt")
        doc2 = DTE.Documents.Item("TextFile1.txt").Object("TextDocument")
        ''doc2 = CType(doc.Object(), TextDocument)


        soln = DTE.Solution


        ' Find the specified project.
        proj = soln.Projects.Item(1)
        pname = proj.FullName.ToString
        pitem = proj.ProjectItems.Item(1)
        iname = pitem.Name
        doc = pitem.Document
        dname = doc.Name


    End Sub


    Sub EditPointExample()
        'Before running this example, open a text document.
        Dim objTD As TextDocument
        Dim objEP As EditPoint

        objTD = DTE.ActiveDocument.Object("TextDocument")
        objEP = objTD.StartPoint.CreateEditPoint
        objEP.Insert("Hello")

        DTE.ItemOperations.NewFile()
        Dim textDoc As TextDocument = CType(DTE.ActiveDocument.Object(), TextDocument)
        Dim editPnt As EditPoint = textDoc.StartPoint.CreateEditPoint()
        Dim i As Integer
        For i = 1 To 10
            editPnt.Insert("Line " & i.ToString() & vbCrLf)
        Next

        If MsgBox("Reverse the order of the lines?", MsgBoxStyle.YesNo) = _
            MsgBoxResult.Yes Then
            Dim textSel As TextSelection = textDoc.Selection

            ' Position insertion point at beginning of "Line 2".
            textSel.StartOfDocument()
            textSel.LineDown()

            ' Reverse the order of the lines by cutting "Line 2", 
            ' pasting it at the start of the document, and then cutting
            ' "Line 3" and pasting it at the start of the document,
            ' and so on.
            For i = 1 To 9
                textSel.LineDown(True)
                textSel.Cut()
                editPnt.StartOfDocument()
                editPnt.Paste()
            Next
        End If

    End Sub

End Module

