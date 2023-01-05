<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptInventoryNet.aspx.cs" Inherits="RealERPWEB.F_12_Inv.RptInventoryNet" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <script type="text/javascript" language="javascript">

        $(document).ready(function () {
                                               
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });


           <%-- var gv = $('#<%=this.gvbillstatus.ClientID %>');
            gv.Scrollable();--%>

        }

      </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
     <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                               
                                <div class="form-group">
                                    <div class="col-md-2 asitCol3 pading5px">
                                        <asp:Label ID="Label4" runat="server"
                                            Text="Project Name:"
                                            CssClass="lblTxt lblName"></asp:Label>
                                    </div>


                                    <div class="col-md-3 asitCol3 pading5px" style="margin-left:-80px;">
                                        <asp:DropDownList ID="ddlProjectName" runat="server"
                                            Width="220px"  CssClass="chzn-select ddlistPull"
                                         >
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1 asitCol1 pading5px" style="margin-left:20px;">
                                        <asp:LinkButton ID="lbtnOk" runat="server" OnClick="lbtnOk_Click"
                                             CssClass="btn btn-primary primaryBtn">Ok</asp:LinkButton>
                                    </div>


                                    

                                </div>
                                 <div class="form-group">
                                    <div class="col-md-5  pading5px" style="margin-left:50px">
                                        <asp:Label ID="lblDate" runat="server" CssClass="lblTxt lblName"
                                            Text="Date:"></asp:Label>

                                        <asp:TextBox ID="txtFDate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>


                                        <asp:Label ID="lbldateTo" runat="server" Font-Bold="True"
                                            Text="Date:" CssClass="smLbl_to" Visible="true"></asp:Label>

                                        <asp:TextBox ID="txttoDate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttoDate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txttoDate"></cc1:CalendarExtender>
                                    </div>
                                       <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPage" runat="server" CssClass="smLbl_to" Text="Page Size"></asp:Label>


                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                </div>


                            </div>
                        </fieldset>    
                        <div class="row">
                            <asp:GridView ID="gvMatStock" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" OnPageIndexChanging="gvMatStock_PageIndexChanging" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>


                                                <asp:Label ID="hlnkgcResDesc" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                    Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false" Target="_blank"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                                    Width="150px">
                                                </asp:Label>

                                            </ItemTemplate>
                                            
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Opening Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lgvOp" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvOpF" runat="server" Width="70px" Style="text-align: right" Font-Bold="true"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Received Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lgvRecamt" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rcvqty")).ToString("#,##0.0000;(#,##0.0000); ")%>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvRecamtF" runat="server" Width="70px" Style="text-align: right" Font-Bold="true"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Transfer In">
                                <ItemTemplate>
                                    <asp:Label ID="lgvTraninqty" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trninqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvTraninqtyF" runat="server" Width="70px" Style="text-align: right" Font-Bold="true"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Transfer Out">
                                <ItemTemplate>
                                    <asp:Label ID="lgvTranoutqty" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnoutqty")).ToString("#,##0.0000;(#,##0.0000); ")%>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>

                                <FooterTemplate>
                                    <asp:Label ID="lgvTranoutqtyF" runat="server" Width="70px" Style="text-align: right" Font-Bold="true"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" />

                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Damage/Lost/Sold">
                                <ItemTemplate>
                                    <asp:Label ID="lgvDamage" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lqty")).ToString("#,##0.0000;(#,##0.0000); ")%>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>

                                <FooterTemplate>
                                    <asp:Label ID="lgvDamageF" runat="server" Width="70px" Style="text-align: right" Font-Bold="true"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Received">
                                <ItemTemplate>
                                    <asp:Label ID="lgvTamt" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trcvqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvTamtF" runat="server" Width="70px" Style="text-align: right" Font-Bold="true"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Issue Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lgvIsuamt" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "issueqty")).ToString("#,##0.0000;(#,##0.0000); ")%>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvIsuamtF" runat="server" Width="70px" Style="text-align: right" Font-Bold="true"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" />

                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                           

                                        
                                        <asp:TemplateField HeaderText="Stock Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgstockqty" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "actstockqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                           
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvrate" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>

                                               
                                                <asp:Label runat="server" ID="lblTotal">Total</asp:Label>

                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                       
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAcSamt" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "actstockamt")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>

                                                <asp:Label runat="server" ID="lblactstktf"></asp:Label>

                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Right" />
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
            <!-- End of Container-->


        </ContentTemplate>
    </asp:UpdatePanel>   


</asp:Content>

