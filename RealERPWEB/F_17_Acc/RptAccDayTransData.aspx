<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptAccDayTransData.aspx.cs" Inherits="RealERPWEB.F_17_Acc.RptAccDayTransData" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


     <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

     
            var gridview = $('#<%=this.gvtranlsit.ClientID %>');
            gridview.Scrollable();

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
                        <fieldset class="scheduler-border fieldset_B">

                            <div class="form-horizontal">
                                <div class="form-group">

                                    <div class="col-md-3 asitCol3 pading5px">

                                        <asp:Label ID="Label22" runat="server" CssClass="lblTxt lblName"
                                            Text="From:"></asp:Label>

                                        <asp:TextBox ID="txtfromdate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtfromdate" TodaysDateFormat=""></cc1:CalendarExtender>



                                    </div>



                                    <div class="col-md-3 asitCol3 pading5px">
                                        <asp:Label ID="Label23" runat="server" CssClass="smLbl_to" Text="To:"></asp:Label>

                                        <asp:TextBox ID="txttodate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy " TargetControlID="txttodate" TodaysDateFormat=""></cc1:CalendarExtender>


                                        <asp:LinkButton ID="lbtnShow" runat="server" OnClick="lbtnShow_Click"
                                            CssClass="btn btn-primary primaryBtn">Show</asp:LinkButton>
                                    </div>
                                </div>

                              <div class="form-group">

                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblcontrolAccHead" runat="server" CssClass="lblTxt lblName" Text="Get Acc. Heads" visible="false"></asp:Label>
                                            <asp:TextBox ID="txtAccSearch" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1" visible="false"></asp:TextBox>

                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="IbtnSearchAcc" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="IbtnSearchAcc_Click" TabIndex="2" visible="false"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="col-md-5 pading5px ">
                                            <asp:DropDownList ID="ddlAccHead" runat="server" CssClass="form-control chzn-select" TabIndex="3" visible="false" Width="300px">
                                            </asp:DropDownList>

                                        </div>




                                    </div>

                                <div class="form-group">
                                    <div class="col-md-3 asitCol3 pading5px">
                                        <asp:Label ID="lblVoucher" runat="server" CssClass="lblTxt lblName" Text="Voucher Type :"></asp:Label>

                                        <asp:DropDownList ID="ddlVouchar" runat="server" Width="73" CssClass="ddlistPull">
                                            <asp:ListItem>BC</asp:ListItem>
                                            <asp:ListItem>BD</asp:ListItem>
                                            <asp:ListItem>CC</asp:ListItem>
                                            <asp:ListItem>CD</asp:ListItem>
                                            <asp:ListItem>CT</asp:ListItem>
                                            <asp:ListItem>JV</asp:ListItem>
                                            <asp:ListItem Selected="True">ALL Voucher</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                 <%--   <div class="col-md-3 asitCol3 pading5px">
                                        <asp:Label ID="lblPage" runat="server" CssClass="smLbl_to" Text="Page Number:"
                                            Visible="False"></asp:Label>

                                        <asp:TextBox ID="txtPageNo" runat="server" CssClass="inputtextbox"
                                            Visible="False"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnSearchPage" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgbtnSearchPage_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>--%>


                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 asitCol3 pading5px">
                                        <asp:Label ID="lblAmount" runat="server" CssClass="lblTxt lblName"
                                            Text="Amount:"></asp:Label>

                                        <asp:DropDownList ID="ddlSrch" runat="server" CssClass="ddlistPull" AutoPostBack="True" Width="73px"
                                            OnSelectedIndexChanged="ddlSrch_SelectedIndexChanged">
                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                            <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                            <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                            <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                            <asp:ListItem Value="&gt;=">Greater Then  Equal</asp:ListItem>
                                            <asp:ListItem Value="between">Between</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-3 asitCol3 pading5px">
                                        <asp:TextBox ID="txtAmount1" runat="server" CssClass="inputtextbox"></asp:TextBox>


                                        <asp:Label ID="lblTo" runat="server" CssClass="smLbl_to"
                                            OnClick="imgbtnSearchVoucher_Click" />
                                    </div>



                                </div>
                      <%--          <div class="form-group">
                                    <asp:Panel runat="server" ID="GridPage"  Visible="false">
                                        <asp:LinkButton ID="imgBtnFirst" CssClass="btn btn-primary primaryBtn" runat="server" OnClick="imgBtnFirst_Click" TabIndex="2"><--Prv</asp:LinkButton>


                                        <asp:LinkButton ID="imgBtnNext" CssClass="btn btn-primary primaryBtn" runat="server" OnClick="imgBtnNext_Click" TabIndex="2">Prv</asp:LinkButton>

                                        <asp:Label ID="lblCurPage" runat="server" CssClass="lblTxt lblName">1</asp:Label>
                                        <asp:LinkButton ID="imgBtnPerv" CssClass="btn btn-primary primaryBtn" runat="server" OnClick="imgBtnPerv_Click" TabIndex="2">Next</asp:LinkButton>

                                        <asp:LinkButton ID="imgBtnLast" CssClass="btn btn-primary primaryBtn" runat="server" OnClick="imgBtnLast_Click" TabIndex="2">Next --> </span></asp:LinkButton>



                                    </asp:Panel>

                                </div>--%>
                            </div>
                        </fieldset>
                    </div>
                    <div class="table table table-responsive">
                        <asp:GridView ID="gvtranlsit" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            AutoGenerateColumns="False" ShowFooter="True"
                            OnRowDataBound="gvtranlsit_RowDataBound">
                            <PagerSettings Position="TopAndBottom" />
                            <PagerStyle VerticalAlign="Top" />

                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo4" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rowid")) %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDate1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Voucher #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvvnum1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvAcRsCode" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acrescode")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                 <asp:TemplateField HeaderText="Description">
                                    <HeaderTemplate>
                                        <table style="width: 47%;">
                                            <tr>
                                                <td class="style58">
                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                        Text="Description" Width="150px" Style="text-align:left"></asp:Label>
                                                </td>
                                                <td class="style60">&nbsp;</td>
                                                <td>
                                                    <asp:HyperLink ID="hlbtnbtbCdataExel" runat="server"  CssClass="btn btn-primary primaryBtn"  Width="100px">Export Exel</asp:HyperLink>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvAcRsDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acresdesc"))+Convert.ToString(DataBinder.Eval(Container.DataItem, "venarr")) %>'
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>



                                
                                <asp:TemplateField HeaderText="Res. Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvInAmt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "inneram")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Debit ">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvDram" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFDram" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="90px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Credit">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvCram" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="txtgvFCram" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="100px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cheque </br>/Ref #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRefnum" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Other</br> Ref #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvOthRefnum" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "srinfo")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Party Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvParyname" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="User Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvpostusername" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postedbydesc")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </div>
                    <div class="form-group">

                        <asp:Label ID="lblCashVoucher" runat="server" CssClass="lblTxt lblName"
                            Text="Cash Voucher:"></asp:Label>

                        <asp:Label ID="lbltoCashVoucher" runat="server" CssClass="lblTxt lblName"></asp:Label>

                        <asp:Label ID="lblBankVoucher" runat="server" CssClass="lblTxt lblName"
                            Text="Bank Voucher:"></asp:Label>

                        <asp:Label ID="lbltoBankVoucher" runat="server" CssClass="lblTxt lblName"></asp:Label>

                        <asp:Label ID="lblContraVoucher" runat="server" Font-Bold="True"
                            CssClass="lblName lblTxt"
                            Text="Contra Voucher:"></asp:Label>

                        <asp:Label ID="lbltoContraVoucher" runat="server" CssClass="lblName lblTxt"></asp:Label>

                        <asp:Label ID="lblJournalVoucher" runat="server" CssClass="lblTxt lblName"
                            Text="Journal Voucher:"></asp:Label>

                        <asp:Label ID="lbltoJournalVoucher" runat="server"></asp:Label>

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
