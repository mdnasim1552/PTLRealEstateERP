<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="ProTargetMonthly.aspx.cs" Inherits="RealERPWEB.F_08_PPlan.ProTargetMonthly" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
  <script language="javascript" type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
  <script language="javascript" type="text/javascript" src="../Scripts/ScrollableGridPlugin.js"></script>
   <script type="text/javascript" language="javascript" src="../Scripts/KeyPress.js"></script>
  
   <script language="javascript" type="text/javascript">

       $(document).ready(function () {
           Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(PageLoad);

       });

       function PageLoad() {
           $("input, select").bind("keydown", function (event) {
               var k1 = new KeyPress();
               k1.textBoxHandler(event);
           });
           var gv = $('#<%=this.gvProTarget.ClientID %>');
           gv.Scrollable();

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
                                <asp:Panel ID="Panel1" runat="server" >
                                        <div class="form-group">
                                                 <div class="col-md-11 pading5px asitCol11">
                                                     <asp:Label ID="lblProjectList" runat="server" CssClass=" lblName lblTxt" Text="Project Name:" ></asp:Label>

                                                     <asp:TextBox ID="txtProjectSearch" runat="server" CssClass=" inputtextbox"></asp:TextBox>

                                                     <asp:LinkButton ID="ImgbtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ImgbtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                    <asp:DropDownList ID="ddlProject" runat="server" Width="300" CssClass="ddlPage"  AutoPostBack="True" onselectedindexchanged="ddlProject_SelectedIndexChanged"  TabIndex="2"></asp:DropDownList>
                                                                                                
                                                    <asp:Label ID="lblProjectDesc" runat="server" CssClass=" inputtextbox" Width="300" Visible="false"></asp:Label>
                                                   
                                                   <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                                      <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                                                            AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="50">
                                                            <ProgressTemplate>
                                                                <asp:Label ID="Label3" runat="server"  Text="Please wait . . . . . . ."  CssClass=" lblName lblTxt"   Width="150px"></asp:Label>
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>

                                                </div>
                                              </div>

                                    <div class="form-group">
                                                 <div class="col-md-11 pading5px asitCol11">
                                                     <asp:Label ID="lblMonth" runat="server" CssClass=" lblName lblTxt"  Text="Month:" ></asp:Label>
                                                    <asp:DropDownList ID="ddlMonth" runat="server"  CssClass="ddlPage"   TabIndex="2"></asp:DropDownList>
                                                   
                                                </div>
                                              </div>
                                </asp:Panel>

                                 <asp:Panel ID="PnlColoumn" runat="server" Visible="false">
                                       <div class="form-group">
                                                 <div class="col-md-11 pading5px asitCol11">
                                                     <asp:Label ID="lblSearchItem" runat="server" CssClass=" lblName lblTxt" Text="Search Item:" ></asp:Label>

                                                     <asp:TextBox ID="txtSearchItem" runat="server" CssClass=" inputtextbox"></asp:TextBox>

                                                     <asp:LinkButton ID="ImgbtnSearchItem" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ImgbtnSearchItem_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                      <asp:Label ID="lblPage" runat="server"  Text="Page Size:"  CssClass="inputtextbox" Visible="False"  Width="300" ></asp:Label>

                                                    <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"  CssClass="ddlPage" onselectedindexchanged="ddlpagesize_SelectedIndexChanged" Visible="False" TabIndex="6">
                                            
                                                            <asp:ListItem Value="15">15</asp:ListItem>
                                                            <asp:ListItem Value="20">20</asp:ListItem>
                                                            <asp:ListItem Value="30">30</asp:ListItem>
                                                            <asp:ListItem Value="50">50</asp:ListItem>
                                                            <asp:ListItem Value="100">100</asp:ListItem>
                                                        </asp:DropDownList>
                                                                                                
                                                   
                                                   
                                                   
                                                </div>
                                              </div>

                                 </asp:Panel>


                            </div>
                            </fieldset>
                        </div>
                    <div class="table table-responsive">
                         <asp:GridView ID="gvProTarget" runat="server"  CssClass=" table-striped table-hover table-bordered grvContentarea"

                            AutoGenerateColumns="False" HeaderStyle-CssClass="HeaderStyle" 
                            onpageindexchanging="gvProTarget_PageIndexChanging" ShowFooter="True" 
                            Width="16px" onrowdeleting="gvProTarget_RowDeleting" AllowPaging="True">
                            <PagerStyle ForeColor="#000" />
                          
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="False" Font-Size="12px" 
                                            style="text-align: right" 
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px">
                                         </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="ItemCode" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvItemcode" runat="server" Font-Bold="False" Font-Size="12px" 
                                            style="text-align: right" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isircode"))%>' Width="80px">
                                         </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="FloorCode" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvFloorcod" runat="server" Font-Bold="False" Font-Size="12px" 
                                            style="text-align: right" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrcod"))%>' Width="40px">
                                         </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:CommandField ShowDeleteButton="True" />
                                <asp:TemplateField HeaderText="Item Description ">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnUpdate" runat="server" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="#000" onclick="lbtnUpdate_Click">Final Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvItemDesc" runat="server" Font-Size="12px" Text='<%# "<B>" + Convert.ToString(DataBinder.Eval(Container.DataItem, "isirdesc")) + "</B>" +
                                                                         (DataBinder.Eval(Container.DataItem, "flrdes").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "isirdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")).Trim(): "") 
                                                                         
                                                                    %>' Width="250px">   </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnTotal0" runat="server" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="#000" onclick="lbtnTotal_Click">Total</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvResUnit" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isirunit")) %>' 
                                            Width="35px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Budgeted Qty">
                               
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvbgdqty" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdqty")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Bal. Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvttrqty" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                   
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Difference">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvdiffqty" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "difqty")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM1">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trqty")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                 
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                               
                            </Columns>
                          <FooterStyle CssClass="grvFooter"/>
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </div>
                    </div>
                </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

