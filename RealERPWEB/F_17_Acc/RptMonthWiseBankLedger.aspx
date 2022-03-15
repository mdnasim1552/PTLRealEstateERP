<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptMonthWiseBankLedger.aspx.cs" Inherits="RealERPWEB.F_17_Acc.RptMonthWiseBankLedger" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {

            var gv1 = $('#<%=this.gvBankLedger.ClientID %>');
            gv1.Scrollable();

            $('.chzn-select').chosen({ search_contains: true });





        }


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
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName"> Bank Name :</asp:Label>
                                        <asp:TextBox ID="txtSrcBank" runat="server" CssClass=" inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="imgbtnFindBank" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnFindBank_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>

                                    </div>

                                    <div class="col-md-4 pading5px">
                                        <asp:DropDownList ID="ddlBankName" runat="server" CssClass="chzn-select form-control  inputTxt" Style="width: 336px">
                                        </asp:DropDownList>

                                    </div>

                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass="btn btn-primary okBtn" Style="margin-left: -50px" OnClick="lnkbtnOk_Click">Ok</asp:LinkButton>

                                    </div>


                                </div>
                                <div class="form-group">
                                    <div class="col-md-5 pading5px">
                                        <asp:Label ID="lbldatefrm" runat="server" CssClass="lblTxt lblName">Date</asp:Label>


                                        <asp:TextBox ID="txtFDate" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtFDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>
                                        <asp:Label ID="lbldateto" runat="server" CssClass="lblTxt smLbl_to" Style="margin-right: 7px;">To</asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server" CssClass="inputTxt inputName inputDateBox" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>

                                    </div>
                                </div>
                                <%-- <div class="form-group" visible="false" >
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPage" runat="server" Text="Size:" CssClass="lblName lblTxt"></asp:Label>

                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" CssClass="ddlPage"
                                            TabIndex="4">
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                            <asp:ListItem Value="150">150</asp:ListItem>
                                            <asp:ListItem Value="200">200</asp:ListItem>
                                            <asp:ListItem Value="300">300</asp:ListItem>
                                           <asp:ListItem Value="300">500</asp:ListItem>
                                            <asp:ListItem Value="300">700</asp:ListItem>
                                            <asp:ListItem Value="300">900</asp:ListItem>

                                        </asp:DropDownList>

                                    </div>
                                    
                                    
                                </div>--%>
                            </div>
                        </fieldset>
                    </div>


                    <asp:GridView ID="gvBankLedger" runat="server" AllowPaging="false" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        AutoGenerateColumns="False"
                        ShowFooter="true" OnRowDataBound="gvBankLedger_RowDataBound">

                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server" Height="16px"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="Monthid" Visible="false">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lblgvmonthid" runat="server"
                                        Text='<%# "<b>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "monthid"))+"</b>" %>'
                                        Width="100px" Font-Size="11px"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>





                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Month" Width="70px"></asp:Label>
                                    <asp:HyperLink ID="hlbtntbCdataExcel" runat="server" CssClass="btn  btn-success btn-xs" ToolTip="Export Excel"><i  class=" fa fa-file-excel-o "></i>
                                    </asp:HyperLink>
                                </HeaderTemplate>

                                <FooterTemplate>
                                    <asp:Label ID="lgvFmonth" runat="server" Font-Bold="True"> </asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:HyperLink ID="lblgvactDesc" runat="server"
                                        Text='<%# "<b>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "monthid1"))+"</b>" %>'
                                        Width="100px" Font-Size="11px"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Opening Amount">
                                <FooterTemplate>
                                    <asp:Label ID="lgvFopnamt" runat="server" Font-Bold="True" Width="80px"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvopnamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;-#,##0; ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Debit Amount">
                                <FooterTemplate>
                                    <asp:Label ID="lgvFdebitamt" runat="server" Font-Bold="True" Width="80px"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvdebitamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trndr")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Credit Amount">

                                <FooterTemplate>
                                    <asp:Label ID="lgvFcreditamt" runat="server" Font-Bold="True" Width="80px"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvcreditamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trncr")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Closing Amount">

                                <FooterTemplate>
                                    <asp:Label ID="lgvFclsamt" runat="server" Font-Bold="True" Width="80px"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvclsamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clsamt")).ToString("#,##0;-#,##0; ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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





        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


