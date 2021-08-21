<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptSalesRegister2.aspx.cs" Inherits="RealERPWEB.F_22_Sal.RptSalesRegister2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
      <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            try {              
                $('.chzn-select').chosen({ search_contains: true });
            }
            catch(e) {
                alert(e);
            }
            var gvSpayment = $('#<%=this.gvSalesRegis.ClientID %>');
            gvSpayment.gridviewScroll({
                width: 1160,
                height: 420,
                arrowsize: 30,
                railsize: 16,
                barsize: 8,
                varrowtopimg: "../Image/arrowvt.png",
                varrowbottomimg: "../Image/arrowvb.png",
                harrowleftimg: "../Image/arrowhl.png",
                harrowrightimg: "../Image/arrowhr.png",
                freezesize: 7
            });
       };
      </script>
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
                             <div class="col-md-3 asitCol2  pading5px">

                                <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName" Font-Bold="True"
                                    Text="Project Name:"></asp:Label>

                                <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputtextbox inputTxt" Width="80px"
                                    BorderStyle="None"></asp:TextBox>
                                

                                <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                            </div>
                            
                            <div class="col-md-3 pading5px asitCol3  pull-left">
                                <asp:DropDownList ID="ddlProjectName" runat="server" CssClass=" form-control inputTxt" Font-Bold="false"                                  >
                                </asp:DropDownList>


                            </div>
                            <div class="col-md-7 pading5px">


                                <asp:Label ID="Label15" runat="server" CssClass=" smLbl_to"
                                    Text="Date:"></asp:Label>


                                <asp:TextBox ID="txtDate" runat="server" CssClass="inputtextbox " Width="80px"
                                    BorderStyle="None"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>                             
                            
                                <asp:Label ID="lblPage" runat="server" CssClass="smLbl_to" Font-Bold="True"
                                    Text="Size:" Visible="False"></asp:Label>

                                <asp:DropDownList ID="ddlpagesize" runat="server" CssClass="ddlPage" AutoPostBack="True"
                                    Font-Bold="True"
                                    OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False"
                                    Width="50px">
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="20">20</asp:ListItem>
                                    <asp:ListItem Value="30">30</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                    <asp:ListItem Value="150">150</asp:ListItem>
                                    <asp:ListItem Value="200">200</asp:ListItem>
                                    <asp:ListItem Value="300">300</asp:ListItem>
                                </asp:DropDownList>
                                 <asp:LinkButton ID="lbtnOk" runat="server" CssClass=" btn btn-primary  okBtn " OnClick="lbtnOk_Click"
                                    Font-Underline="False">Ok</asp:LinkButton>
                           
                            </div>
                         

                        </div>


                  
                      
                    </div>
                </fieldset>
                <div class=" table-responsive">                                       
                            <asp:GridView ID="gvSalesRegis" runat="server" AutoGenerateColumns="False"
                                ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea" AllowPaging="True"
                                OnPageIndexChanging="gvSalesRegis_PageIndexChanging">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="false"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Sales Person">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvperson" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "salperson")) %>'
                                                Width="100px" Font-Bold="false"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Selling Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvsaldat" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "schdate")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Project Name">

                                        <ItemTemplate>
                                            <asp:Label ID="lgPadcDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvditem" runat="server" Text="Total" Font-Bold="True" HorizontalAlign="Left" Font-Size="12px"
                                                ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Unit ">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvUnit" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "munit")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Client Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvTqty" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cusname")) %>'
                                                Width="120px" Style="text-align: left"></asp:Label>
                                        </ItemTemplate>                                       
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sales Value">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvSalAmt" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tuamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="100px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                  <FooterTemplate>
                                            <asp:Label ID="lgvFSalAmt" runat="server" Font-Bold="True" 
                                                ForeColor="Black"  Width="100px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Collection Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvSCol" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "colamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="100px" Style="text-align: right;"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFColl" runat="server" Font-Bold="True" 
                                                ForeColor="Black"  Width="100px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
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


</asp:Content>


