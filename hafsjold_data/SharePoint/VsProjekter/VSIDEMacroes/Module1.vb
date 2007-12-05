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

    Sub GenerateTypes()
        Const COREFILE = "ProvPur"
        Const FILENAME_TYPES = "types.xml"
        Const FILENAME_DK = COREFILE & ".da-dk.resx"
        Const FILENAME_UK = COREFILE & ".resx"

        Dim XMLFileTYPES As TextDocument = DTE.Documents.Item(FILENAME_TYPES).Object("TextDocument")
        Dim XMLFileTYPEStextSel As TextSelection = XMLFileTYPES.Selection
        XMLFileTYPEStextSel.SelectAll()
        Dim strXML As String = Regex.Replace(XMLFileTYPEStextSel.Text, "xmlns=""[^""]*""", "")
        Dim docTYPES As System.Xml.XmlDocument = New System.Xml.XmlDocument()
        docTYPES.LoadXml(strXML)
        XMLFileTYPEStextSel = Nothing


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
        Dim dsTypeItems As System.Data.DataSet = myList.getListData("ProPurType")
        Dim Type_rows As DataTable = dsTypeItems.Tables("row")
        Dim dsFieldItems As System.Data.DataSet = myList.getListData("ProPurList")
        Dim Field_rows As DataTable = dsFieldItems.Tables("row")

        For Each Type_row As DataRow In Type_rows.Rows

            Dim Type_Id As Integer = CType(Type_row("ows_ID"), Integer)
            Dim Type_SysName As String = CType(Type_row("ows_Title"), String)
            Dim Type_Fields() As String = Regex.Split(CType(Type_row("ows_Felter"), String), ";#")
            Dim Type_BasedOn As String = CType(Type_row("ows_BasedOn"), String)
            Dim Type_DisplayNameDK As String = CType(Type_row("ows_DisplayNameDK"), String)
            Dim Type_DisplayNameUK As String = CType(Type_row("ows_DisplayNameUK"), String)
            Dim Type_Comment As String = CType(Type_row("ows_Comment"), String)
            Dim Type_Guid As String = CType(Type_row("ows_Type_GUID"), String)

            Dim ContentTypeID As String
            Select Case Type_BasedOn
                Case "Element"
                    ContentTypeID = "0x01" & "00" & Type_Guid
                Case "Annoncering"
                    ContentTypeID = "0x0104" & "00" & Type_Guid
                Case "Hyperlink"
                    ContentTypeID = "0x0105" & "00" & Type_Guid
                Case "'Kontaktperson"
                    ContentTypeID = "0x0106" & "00" & Type_Guid
                Case "'Meddelelse"
                    ContentTypeID = "0x0107" & "00" & Type_Guid
                Case "'Opgave"
                    ContentTypeID = "0x0108" & "00" & Type_Guid
                Case "'Problem"
                    ContentTypeID = "0x0103" & "00" & Type_Guid
                Case Else
                    ContentTypeID = "Error" & "00" & Type_Guid
            End Select

            Dim dataTYPES As System.Xml.XmlElement = createTypeElement(docTYPES, ContentTypeID, Type_SysName, COREFILE, Type_Fields, Field_rows)
            Dim dataDK As System.Xml.XmlElement = createDataElement(docDK, Type_SysName, Type_DisplayNameDK, Type_Comment)
            Dim dataUK As System.Xml.XmlElement = createDataElement(docUK, Type_SysName, Type_DisplayNameUK, Type_Comment)

        Next Type_row

        strXML = Regex.Replace(docTYPES.OuterXml, "<Elements>", "<Elements xmlns=""http://schemas.microsoft.com/sharepoint/"">")

        Dim starteditPntTYPES As EditPoint = XMLFileTYPES.StartPoint.CreateEditPoint()
        Dim endeditPntTYPES As EditPoint = XMLFileTYPES.StartPoint.CreateEditPoint
        starteditPntTYPES.StartOfDocument()
        endeditPntTYPES.EndOfDocument()
        starteditPntTYPES.Delete(endeditPntTYPES)
        starteditPntTYPES.Insert(strXML)

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

    Private Function createTypeElement( _
            ByRef pdoc As System.Xml.XmlDocument, _
            ByVal pid As String, _
            ByVal pname As String, _
            ByVal pcore As String, _
            ByVal pfields() As String, _
            ByRef pfieldstable As DataTable _
        ) As System.Xml.XmlElement

        Dim contenttype As System.Xml.XmlElement = pdoc.CreateElement("ContentType")
        contenttype.SetAttribute("ID", pid)
        contenttype.SetAttribute("Name", pname)
        contenttype.SetAttribute("Group", "$Resources:" & pcore & ",TypesGroupName;")
        contenttype.SetAttribute("Description", "$Resources:" & pcore & "," & pname & ";")
        contenttype.SetAttribute("Version", "0")

        Dim fieldrefs As System.Xml.XmlElement = pdoc.CreateElement("FieldRefs")
        contenttype.AppendChild(fieldrefs)


        For Each Field_row As DataRow In pfieldstable.Rows
            Dim Field_Id As Integer = CType(Field_row("ows_ID"), Integer)
            For i As Integer = 0 To pfields.Length - 1 Step 2
                If Field_Id = pfields(i) Then
                    Dim Field_SysName As String = CType(Field_row("ows_Title"), String)
                    Dim Field_KolonneType As String = CType(Field_row("ows_KolonneType"), String)
                    Dim Field_DisplayNameDK As String = CType(Field_row("ows_DisplayNameDK"), String)
                    Dim Field_DisplayNameUK As String = CType(Field_row("ows_DisplayNameUK"), String)
                    Dim Field_Comment As String = CType(Field_row("ows_FieldName"), String)
                    Dim Guid As String = CType(Field_row("ows_GUID0"), String)

                    Dim fieldref As System.Xml.XmlElement = pdoc.CreateElement("FieldRef")
                    fieldref.SetAttribute("ID", Guid)
                    fieldref.SetAttribute("Name", Field_SysName)
                    fieldrefs.AppendChild(fieldref)
                    Exit For
                End If
            Next i
        Next Field_row

        Dim elements As System.Xml.XmlNode = pdoc.SelectSingleNode("//Elements")
        Dim filter As String = "//ContentType[@ID=""" & pid & """]"
        Dim old_contenttype As System.Xml.XmlNode = elements.SelectSingleNode(filter)

        If old_contenttype Is Nothing Then
            elements.AppendChild(contenttype)
        Else
            elements.ReplaceChild(contenttype, old_contenttype)
        End If

        Return contenttype
    End Function


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

        'docs = DTE.Documents
        'doc = DTE.Documents.Item("TextFile1.txt")
        'doc2 = DTE.Documents.Item("TextFile1.txt").Object("TextDocument")
        ''doc2 = CType(doc.Object(), TextDocument)


        soln = DTE.Solution
        Dim XMLFileTYPES As TextDocument = DTE.Documents.Item("mha.txt").Object("TextDocument")


        ' Find the specified project.
        proj = soln.Projects.Item(1)
        pname = proj.Name.ToString
        'For Each obj As Object In proj.Collection.Properties
        'Dim xx As String = "ss"
        'Next
        For i As Integer = 1 To proj.ProjectItems.Count
            pitem = proj.ProjectItems.Item(i)
            iname = pitem.Name
            Dim ant As Integer = pitem.ProjectItems.Count
        Next
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

    ' Before running, create a new project or open an existing project.
    Sub ListProj()
        Dim proj As Project = DTE.ActiveSolutionProjects(0)
        Dim win As Window = DTE.Windows.Item(Constants.vsWindowKindCommandWindow)
        ListProjAux(proj.ProjectItems(), 0)
    End Sub

    Sub ListProjAux(ByVal projitems As ProjectItems, ByVal Level As Integer)
        Dim projitem As ProjectItem
        For Each projitem In projitems
            Dim msg As String = "Project item: " & projitem.Name & " Level. " & Level & " " & projitem.Kind
            ' Recurse if the project item has sub-items...
            Dim projitems2 As ProjectItems
            projitems2 = projitem.ProjectItems
            Dim notsubcoll As Boolean = projitems2 Is Nothing
            If Not notsubcoll Then
                ListProjAux(projitems2, Level + 1)
            End If
            projitems2.AddFolder("ddd")
        Next
    End Sub

End Module

