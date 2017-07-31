<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ConvertPdfs.aspx.cs" Inherits="ConvertPdfs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <h2>Convert Form PDFs</h2>
    <p>
        Convert from old pdf templates to new templates.
    </p>
    <p>
        <b>Select Old PDF:</b>
        <asp:DropDownList ID="ddlPDFs" runat="server">
        </asp:DropDownList>
    </p>
    <p>
        <b>Select Converted PDF:</b>
        <asp:DropDownList ID="ddlPDFsConverted" runat="server">
        </asp:DropDownList>
    </p>
    <p>
<%--        <asp:Button ID="btnShowFields" runat="server" Text="Show Old Form Fields" />
        <asp:Button ID="btnShowFieldsConverted" runat="server" Text="Show Converted Form Fields" />--%>
        <asp:Button ID="btnGenerateSampleConverted" runat="server" Text="Generate Samples" OnClick="btnGenerateSampleConverted_Click" />

        <%--        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnGeneratePDF" runat="server" Text="Generate Sample PDF"
            OnClick="btnGeneratePDF_Click" />
        &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnSerializeFields" runat="server" Text="Serialize Form Fields" OnClick="btnSerializeFields_Click" />--%>
    </p>
    <hr />

    <asp:BulletedList ID="blFields" runat="server" BulletStyle="Numbered">
    </asp:BulletedList>
</asp:Content>

