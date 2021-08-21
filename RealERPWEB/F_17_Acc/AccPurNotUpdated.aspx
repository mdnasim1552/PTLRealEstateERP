<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AccPurNotUpdated.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccPurNotUpdated" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {


            $('.chzn-select').chosen({ search_contains: true });





        };
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>
                        <div id="loader">
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="lading"></div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-4 pading5px">

                                        <asp:Label ID="lblDate" runat="server" CssClass="lblTxt lblName" Text="As on Date"></asp:Label>
                                        <asp:TextBox ID="txtdate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>



                                    </div>

                                    <div class="col-md-3 pading5px pull-right">

                                        <asp:Label ID="lblmsg" CssClass="btn-danger btn disabled" runat="server" Visible="false"></asp:Label>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblcontrolAccHead" runat="server" CssClass="lblTxt lblName" Text="Trans. From Code"></asp:Label>
                                        <asp:TextBox ID="txtserceacc" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="imgbtnFindAccount" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnFindAccount_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>

                                    </div>

                                    <div class="col-md-4 pading5px ">
                                        <asp:DropDownList ID="ddlSubCon" runat="server" CssClass="form-control inputTxt chzn-select">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>


                                    </div>

                                </div>
                            </div>
                            </fieldset>
                    </div>
                    <asp:GridView ID="dgvPurbill" runat="server" AutoGenerateColumns="False" BackColor="White" CssClass=" table-striped table-hover table-bordered grvContentarea" 
                       ShowFooter="True" Width="580px">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="serialnoid" runat="server" CssClass="GridLebel" ForeColor="Black" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                        Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Supplier">
                                <ItemTemplate>
                                    <asp:Label ID="lblSupplier" runat="server" CssClass="GridLebelL" ForeColor="Black"
                                        Text='<%# "<B>" + Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) + "</B>" +
                                                                         (DataBinder.Eval(Container.DataItem, "pactdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")).Trim(): "")  + 
                                                                         (DataBinder.Eval(Container.DataItem, "billno1").ToString().Trim().Length>0 ?
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")).Trim().Length > 0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+
                                                                          Convert.ToString(DataBinder.Eval(Container.DataItem, "billno1")).Trim() : "")
                                                                    %>'
                                        Width="300px">





                                            
                                            
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="txtTotal" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                        BorderStyle="None" BorderWidth="1px" CssClass="GridTextbox" Font-Bold="True"
                                        Font-Size="12px" ForeColor="#000" ReadOnly="true" Text="Total : " Width="70px"></asp:Label>
                                </FooterTemplate>
                                <HeaderStyle Font-Size="12px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Date" ItemStyle-Font-Size="9px">
                                <ItemTemplate>
                                    <asp:Label ID="lblDate" runat="server" CssClass="GridLebelL" ForeColor="Black"
                                        Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tdate")) %>'
                                        Width="90px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Size="11px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Ref. No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblRedno" runat="server" CssClass="GridLebelL" ForeColor="Black" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billref")) %>'
                                        Width="90px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Amount" ItemStyle-Font-Size="11px">
                                <ItemTemplate>
                                    <asp:Label ID="lblamt" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                        BorderStyle="None" BorderWidth="1px" CssClass="GridTextbox" ReadOnly="True" ForeColor="Black"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="txtTgvAmt" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                        BorderStyle="None" BorderWidth="1px" CssClass="GridTextbox" Font-Bold="True"
                                        Font-Size="12px" ForeColor="#000" ReadOnly="true" Width="70px"></asp:Label>
                                </FooterTemplate>
                                <HeaderStyle Font-Size="12px" />
                                <ItemStyle Font-Size="11px" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                        </Columns>

                        <FooterStyle CssClass="grvFooter" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                    </asp:GridView>
                </div>
            </div>



            
            <table>
                <tr>
                    <td></td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
