
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptBillStatus.aspx.cs" Inherits="RealERPWEB.F_16_Bill.RptBillStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    
    
      <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function pageLoaded() {
            try {
                $('.chzn-select').chosen({ search_contains: true });
                $('#<%=this.gvSubBill.ClientID%>')
                    .gridviewScroll({
                        width: 1165,
                        height: 420,
                        arrowsize: 30,
                        railsize: 16,
                        barsize: 8,
                        headerrowcount: 2,
                        freezeFooter: true,
                        varrowtopimg: "../Image/arrowvt.png",
                        varrowbottomimg: "../Image/arrowvb.png",
                        harrowleftimg: "../Image/arrowhl.png",
                        harrowrightimg: "../Image/arrowhr.png",
                        freezesize: 5


                    });
            }

            catch (e) {

                alert(e.message);

            }

        }
      </script>
    
    <style type="text/css">

        .grpheader {
        
        color:maroon;
        font-weight:bold;
        font-size:14px;
        }

    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-3 asitCol3 pading5px">

                                        <asp:Label ID="Label5" runat="server"
                                            Text="From:" CssClass="lblTxt lblName"></asp:Label>

                                        <asp:TextBox ID="txtfrmDate" runat="server" CssClass="inputtextbox"
                                            Font-Bold="True" BorderStyle="None"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfrmDate"></cc1:CalendarExtender>
                                    </div>
                                    <div class="col-md-4 asitCol4 pading5px">


                                        <asp:Label ID="Label6" runat="server" Text="To:" Font-Bold="True" CssClass="smLbl_to"></asp:Label>

                                        <asp:TextBox ID="txttoDate" runat="server" CssClass="inputtextbox" Font-Bold="True"
                                            Width="80px" BorderStyle="None"></asp:TextBox>

                                       <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttoDate"></cc1:CalendarExtender>

                                       

                                    </div>
                                  
                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 asitCol3 pading5px">
                                        <asp:Label ID="Label4" runat="server" Text="Project Name:"
                                            CssClass="lblTxt lblName"></asp:Label>

                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_OnClick" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 asitCol4 pading5px">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" Font-Bold="True" CssClass="ddlistPull"
                                            AutoPostBack="True">
                                        </asp:DropDownList>

                                        <asp:LinkButton ID="lbtnOk" runat="server" OnClick="lbtnOk_Click" CssClass="btn btn-primary primaryBtn">Ok</asp:LinkButton>
                                        
                                    </div>
                                    <div class="col-md-4 asitCol4 pading5px">
                                         <asp:Label ID="lblPage" Visible="False" runat="server" Text="Page:" CssClass="lblTxt lblName"></asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                                     Font-Bold="True" Font-Size="12px"
                                                    OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False">
                                                    <asp:ListItem Value="10">10</asp:ListItem>
                                                    <asp:ListItem Value="15">15</asp:ListItem>
                                                    <asp:ListItem Value="20">20</asp:ListItem>
                                                    <asp:ListItem Value="30">30</asp:ListItem>
                                                    <asp:ListItem Value="50">50</asp:ListItem>
                                                    <asp:ListItem Value="100">100</asp:ListItem>
                                                    <asp:ListItem Value="150">150</asp:ListItem>
                                                    <asp:ListItem Value="200">200</asp:ListItem>
                                                    <asp:ListItem Value="300">300</asp:ListItem>
                                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                   
                                <div class="table-responsive">
                                <asp:GridView ID="gvSubBill" runat="server"
                                AutoGenerateColumns="False"
                               PageSize="20"
                                ShowFooter="True" Width="752px" CssClass="table-responsive table-striped table-hover table-bordered grvContentarea"  OnRowDataBound="gvSubBill_RowDataBound" >
                                <PagerSettings PageButtonCount="20" Mode="NumericFirstLast"  />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="False" Font-Size="12px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Floor">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvRptFlr1" runat="server" Font-Bold="False" Font-Size="12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Work Description">
                                       

                                         <FooterTemplate>
                                            <asp:Label ID="lblgvFTotal" runat="server" BackColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Bold="True" Font-Size="11px" Height="16px"
                                                Style="text-align: right" Width="60px" Text="Total"></asp:Label>
                                        </FooterTemplate>

                                        <ItemTemplate>

                                            <asp:Label ID="lblgvRptRes1" runat="server" Font-Bold="False" Font-Size="12px"                                              
                                                
                                                Text='<%# "<span class=grpheader>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</span>"+
                                                                         (DataBinder.Eval(Container.DataItem, "isirdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                        
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "isirdesc")).Trim(): "") 
                                                                         
                                                                    %>'
                                                   
                                                    
                                                    
                                                  
                                                
                                                  Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <FooterStyle Font-Bold="true" Font-Size="14px" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvRptUnit1" runat="server" Font-Bold="False" Font-Size="12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                Width="30px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Qty">

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvbgdqty" runat="server" BackColor="Transparent" BorderStyle="None"
                                                BorderWidth="1px" Font-Size="11px" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                   
                                     <asp:TemplateField HeaderText="Rate">

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvordrate" runat="server" BackColor="Transparent" BorderStyle="None"
                                                BorderWidth="1px" Font-Size="11px" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Amount">
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFordam" runat="server" BackColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Bold="True" Font-Size="11px" Height="16px"
                                                Style="text-align: right" Width="70px"></asp:Label>
                                        </FooterTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lblordam" runat="server" BackColor="Transparent" BorderStyle="None"
                                                BorderWidth="1px" Font-Size="11px" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle Font-Size="12px" HorizontalAlign="Right" />

                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText=" Quantity ">

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvpreqty" runat="server" BackColor="Transparent" BorderStyle="None"
                                                BorderWidth="1px" Font-Size="11px" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                    </asp:TemplateField>


                                     
                                       <asp:TemplateField HeaderText=" Amount">
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFprebam" runat="server" BackColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Bold="True" Font-Size="11px" Height="16px"
                                                Style="text-align: right" Width="60px"></asp:Label>
                                        </FooterTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lblprebam" runat="server" BackColor="Transparent" BorderStyle="None"
                                                BorderWidth="1px" Font-Size="11px" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pream")).ToString("#,##0;(#,##0); ")  %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle Font-Size="12px" HorizontalAlign="Right" />

                                    </asp:TemplateField>


                                     <asp:TemplateField HeaderText=" Quantity ">

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvcurqty" runat="server" BackColor="Transparent" BorderStyle="None"
                                                BorderWidth="1px" Font-Size="11px" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                    </asp:TemplateField>


                                       <asp:TemplateField HeaderText=" Amount">
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFcuram" runat="server" BackColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Bold="True" Font-Size="11px" Height="16px"
                                                Style="text-align: right" Width="60px"></asp:Label>
                                        </FooterTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lblcuram" runat="server" BackColor="Transparent" BorderStyle="None"
                                                BorderWidth="1px" Font-Size="11px" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curam")).ToString("#,##0;(#,##0); ")  %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle Font-Size="12px" HorizontalAlign="Right" />

                                    </asp:TemplateField>



                                     <asp:TemplateField HeaderText=" Quantity ">

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtbillqty" runat="server" BackColor="Transparent" BorderStyle="None"
                                                BorderWidth="1px" Font-Size="11px" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tbillqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                    </asp:TemplateField>


                                       <asp:TemplateField HeaderText=" Amount">
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFtbillam" runat="server" BackColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Bold="True" Font-Size="11px" Height="16px"
                                                Style="text-align: right" Width="60px"></asp:Label>
                                        </FooterTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lbltbillam" runat="server" BackColor="Transparent" BorderStyle="None"
                                                BorderWidth="1px" Font-Size="11px" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tbillam")).ToString("#,##0;(#,##0); ")  %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle Font-Size="12px" HorizontalAlign="Right" />

                                    </asp:TemplateField>



                                    
                                     <asp:TemplateField HeaderText=" Quantity ">

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvbalqty" runat="server" BackColor="Transparent" BorderStyle="None"
                                                BorderWidth="1px" Font-Size="11px" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                    </asp:TemplateField>


                                     <asp:TemplateField HeaderText="Amount">
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFbalam" runat="server" BackColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Bold="True" Font-Size="11px" Height="16px"
                                                Style="text-align: right" Width="70px"></asp:Label>
                                        </FooterTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lblballam" runat="server" BackColor="Transparent" BorderStyle="None"
                                                BorderWidth="1px" Font-Size="11px" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balam")).ToString("#,##0;(#,##0); ")  %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle Font-Size="12px" HorizontalAlign="Right" />

                                    </asp:TemplateField>

                                    


                                      <asp:TemplateField HeaderText="Rate">

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvbgdrate" runat="server" BackColor="Transparent" BorderStyle="None"
                                                BorderWidth="1px" Font-Size="11px" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    

                                       <asp:TemplateField HeaderText=" Budgeted Amount">
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFbgdam" runat="server" BackColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Bold="True" Font-Size="11px" Height="16px"
                                                Style="text-align: right" Width="60px"></asp:Label>
                                        </FooterTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lblbgdam" runat="server" BackColor="Transparent" BorderStyle="None"
                                                BorderWidth="1px" Font-Size="11px" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdam")).ToString("#,##0;(#,##0); ")  %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle Font-Size="12px" HorizontalAlign="Right" />

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
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>



