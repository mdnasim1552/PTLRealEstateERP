<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="GrpPostDatedChequs.aspx.cs" Inherits="RealERPWEB.F_45_GrAcc.GrpPostDatedChequs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            var gv1 = $('#<%=this.dgv1.ClientID %>');


            gv1.Scrollable();


            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });
        }

    </script>

    



 <%--   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
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

                                <asp:Panel ID="P1" runat="server">
                                  

                                    <fieldset class="scheduler-border fieldset_A">
                                        <div class="form-horizontal">
                                           

                                            <div class=" form-group">

                                                <div class="col-md-6 pading5px asitCol6">
                                                    <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName" Text="Form"></asp:Label>
                                                    <asp:TextBox ID="txtfrmdate" runat="server" AutoPostBack="True" CssClass="inputTxt inputDateBox" TabIndex="6"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server"
                                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>

                                                    <asp:Label ID="lbltodate" runat="server" CssClass="lblTxt lblName" Text="To"></asp:Label>
                                                    <asp:TextBox ID="txttodate" runat="server" AutoPostBack="True" CssClass="inputTxt inputDateBox" TabIndex="7"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                                                    <asp:LinkButton ID="lnkOk" runat="server" CssClass="btn btn-primary primaryBtn okBtn" OnClick="lnkOk_Click" TabIndex="8">Ok</asp:LinkButton>
                                                </div>

                                            </div>

                                        </div>
                                    </fieldset>

                                    


                                </asp:Panel>


                            </div>
                        </fieldset>

                    </div>
                    <div class="row">
                        <div class=" table-responsive">
                            <asp:GridView ID="dgv1" runat="server" AutoGenerateColumns="False"
                                OnRowDataBound="dgv1_RowDataBound" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rowid")) %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                    
                                     <asp:TemplateField HeaderText="Comp Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvAccCOMNAM" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "COMNAM")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="AC.Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvAccCod" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                                Width="49px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cat.Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgcatCod" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactcode")) %>'
                                                Width="49px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ResCode" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lgcUcode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bank Name">
                                        <HeaderTemplate>
                                            <table style="width: 180px;">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Bank Name"
                                                            Width="100px"></asp:Label>
                                                    </td>
                                                    <td class="style60">
                                                        <asp:HyperLink ID="hlbtnbtbCdataExel" runat="server" BackColor="#000066"
                                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                            ForeColor="White" Style="text-align: center" Width="90px" Target="_blank">Export Exel</asp:HyperLink>
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvbankname" runat="server" Font-Bold="true"
                                                Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc"))  %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Voucher #" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvPVnum" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Issue #">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvvounum1" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvPVDate" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Acc. Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lgactdesc" runat="server"
                                                Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "")  %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                           
                                            <asp:Label ID="lgvisunum" runat="server"
                                                Text="Grand Total" style="float:right" 
                                                Width="70px"></asp:Label>

                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Issue Ref">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvisunum" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isunum")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cheque No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvchnono" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequeno")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cheque Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvchdat" runat="server" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequedat")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvcramt" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cramt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFCrAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                        </FooterTemplate>

                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dr. Amt" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvdramt" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dramt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Party Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvParName" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Bill No">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvBill" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
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
                </div>
            </div>
      <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

