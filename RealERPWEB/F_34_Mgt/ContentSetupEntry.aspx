<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="ContentSetupEntry.aspx.cs" Inherits="RealERPWEB.F_34_Mgt.ContentSetupEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>





        function insertMetachars(sStartTag, sEndTag) {
            var bDouble = arguments.length > 1,

                oMsgInput = document.getElementById("<%= txtdescBn.ClientID %>");
            nSelStart = oMsgInput.selectionStart,
                nSelEnd = oMsgInput.selectionEnd,
                sOldText = oMsgInput.value;
            oMsgInput.value = sOldText.substring(0, nSelStart) + (bDouble ? sStartTag + sOldText.substring(nSelStart, nSelEnd) + sEndTag : sStartTag) + sOldText.substring(nSelEnd);
            oMsgInput.setSelectionRange(bDouble || nSelStart === nSelEnd ? nSelStart + sStartTag.length : nSelStart, (bDouble ? nSelEnd : nSelStart) + sStartTag.length);
            oMsgInput.focus();
        };

        function insertMetachars_en(sStartTag, sEndTag) {

            var bDouble = arguments.length > 1,
                oMsgInput = document.getElementById("<%= txtdesceng.ClientID %>");

            nSelStart = oMsgInput.selectionStart, nSelEnd = oMsgInput.selectionEnd, sOldText = oMsgInput.value;
            oMsgInput.value = sOldText.substring(0, nSelStart) + (bDouble ? sStartTag + sOldText.substring(nSelStart, nSelEnd) + sEndTag : sStartTag) + sOldText.substring(nSelEnd);
            oMsgInput.setSelectionRange(bDouble || nSelStart === nSelEnd ? nSelStart + sStartTag.length : nSelStart, (bDouble ? nSelEnd : nSelStart) + sStartTag.length);
            oMsgInput.focus();
        }


    </script>



    <div class="card card-fluid container-data mt-5" id='printarea' style="min-height: 600px;">
        <div class="card-body">

            <div class="row">
                <div class="col-md-1">
                    <asp:LinkButton ID="lnkTag" runat="server" CssClass="btn btn-xs btn-success okbtn" OnClick="lnkTag_Click">Create Tag</asp:LinkButton>
                </div>
            </div>

            <div class="col-md-8">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label class="col-form-label col-md-2">SMS For</label>
                        <div class="col-md-10">
                            <asp:DropDownList ID="ddlSMSfor" runat="server" CssClass="form-control box custom-input">
                                <asp:ListItem>Requirement Send</asp:ListItem>
                                <asp:ListItem>Quotation Send</asp:ListItem>

                            </asp:DropDownList>
                        </div>

                    </div>

                    <div class="form-group">
                        <label class="col-form-label col-md-2">Title EN</label>
                        <div class="col-md-10">
                            <asp:TextBox ID="TxtTitle" ClientIDMode="Static" CssClass="form-control custom-input" placeholder="Enter your Eng Title" runat="server"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="rqxValidationFName" runat="server" Display="Dynamic" CssClass="ValidationError" ErrorMessage="*Field Is Requied" ControlToValidate="TxtTitle" ForeColor="Red" ValidationGroup="savcheck"></asp:RequiredFieldValidator>--%>
                            <%--  <input type="text" class="form-control custom-input hidden" />--%>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-form-label col-md-2">Title BN</label>
                        <div class="col-md-10">
                            <asp:TextBox ID="txtilebn" ClientIDMode="Static" CssClass="form-control custom-input" placeholder="Enter your Title Bangla" runat="server"></asp:TextBox>
                            <%--    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" CssClass="ValidationError" ErrorMessage="*Field Is Requied" ControlToValidate="TxtTitle" ForeColor="Red" ValidationGroup="savcheck"></asp:RequiredFieldValidator>--%>
                            <%--<input type="text" class="form-control custom-input hidden" />--%>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-form-label col-md-2">Tags</label>
                        <div class="col-md-10">
                            <asp:Repeater ID="rptsmsTags" runat="server" OnItemDataBound="rptsmsTags_ItemDataBound">

                                <ItemTemplate>
                                    <span class="intLink" onclick="insertMetachars('<%# Eval("tagname") %>');"><%# Eval("tagname") %></span>

                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>

                    </div>
                    <div class="form-group">
                        <label class="col-form-label col-md-2">Description BN</label>
                        <div class="col-md-10">

                            <asp:TextBox ID="txtdescBn" name="txtOffer" Rows="8" CssClass="form-control" ClientIDMode="Static" runat="server" TextMode="MultiLine"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" CssClass="ValidationError" ErrorMessage="*Field Is Requied" ControlToValidate="txtdesceng" ForeColor="Red" ValidationGroup="txtdescBn"></asp:RequiredFieldValidator>--%>
                        </div>

                    </div>
                    <div class="form-group">
                        <label class="col-form-label col-md-2">Tags</label>
                        <div class="col-md-10">
                            <asp:Repeater ID="rptsmsEnTags" runat="server" OnItemDataBound="rptsmsEnTags_ItemDataBound">
                                <ItemTemplate>
                                    <span class="intLink" onclick="insertMetachars_en('<%# Eval("tagname") %>');"><%# Eval("tagname") %></span>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>

                    </div>
                    <div class="form-group">
                        <label class="col-form-label col-md-2">Description EN</label>
                        <div class="col-md-10">

                            <asp:TextBox ID="txtdesceng" name="txtOffer" Rows="8" CssClass="form-control" ClientIDMode="Static" runat="server" TextMode="MultiLine"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" CssClass="ValidationError" ErrorMessage="*Field Is Requied" ControlToValidate="txtdesceng" ForeColor="Red" ValidationGroup="savcheck"></asp:RequiredFieldValidator>

                        </div>

                    </div>

                    <div class="form-group">
                        <label class="col-form-label col-md-2">SMS Format</label>
                        <div class="col-md-10">
                            <asp:DropDownList ID="ddlFormat" runat="server" CssClass="form-control box custom-input">
                                <asp:ListItem Value="1001">Bangla</asp:ListItem>
                                <asp:ListItem Value="1002">English</asp:ListItem>

                            </asp:DropDownList>
                        </div>

                    </div>

                    <div class="form-group">
                        <div class="col-md-10 col-md-offset-2">
                            <asp:LinkButton ID="lnkSave" runat="server" ValidationGroup="savcheck" OnClick="lnkSave_Click" CssClass="btn btn-primary okBtn">Save</asp:LinkButton>
                        </div>
                    </div>
                </div>

            </div>


        </div>
    </div>
</asp:Content>
