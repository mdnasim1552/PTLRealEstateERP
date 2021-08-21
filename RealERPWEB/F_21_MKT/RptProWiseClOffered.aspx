<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptProWiseClOffered.aspx.cs" Inherits="RealERPWEB.F_21_MKT.RptProWiseClOffered" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <style  type="text/css">
       

    </style>
    
  
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

                        <fieldset class="scheduler-border fieldset_B grpChekBox">

                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3 ">

                                        <asp:Label ID="lbldate" runat="server" CssClass="lblTxt lblName" Text="Date:"></asp:Label>
                                        <asp:TextBox ID="txtDate" runat="server" CssClass="inputTxt inpPixedWidth"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDate">
                                        </cc1:CalendarExtender>


                                        <asp:Label ID="lblbasis" runat="server" Text="Basis:" TabIndex="1" CssClass=" smLbl_to" Visible="False"></asp:Label>

                                        </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:CheckBoxList ID="chkboxlist" runat="server" RepeatDirection="Horizontal" 
                                            Visible="False">
                                            <asp:ListItem>Highest Offer</asp:ListItem>
                                            <asp:ListItem >Highest Booking</asp:ListItem>

                                        </asp:CheckBoxList>




                                    </div>
                                    <div class="col-md-1 pading5px">
                                    </div>
                                    <div class="col-md-1 pading5px">
                                    </div>


                                    <div class="clearfix"></div>


                                </div>

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblProName" runat="server" CssClass="lblTxt lblName" Text="Project Name:"></asp:Label>

                                        <asp:TextBox ID="txtSrcPro" runat="server" TabIndex="3" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnFindProject" runat="server" OnClick="ibtnFindProject_Click" TabIndex="4" CssClass="btn btn-primary srearchBtn  "><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>
                                        </div>
                                    </div>

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" AutoPostBack="True" CssClass="form-control inputTxt chzn-select"
                                            TabIndex="5">
                                        </asp:DropDownList>
                                        
                                    </div>
                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary okBtn" TabIndex="9"></asp:LinkButton>

                                    </div>
                                    <div class="clearfix"></div>
                                </div>

                                <asp:Panel ID="PanelCapacity" runat="server" Visible="False">
                                    <div class="form-group">

                                        <div class="col-md-6 pading5px ">


                                            <asp:Label ID="lblAmount" runat="server" CssClass="lblTxt lblName" Text="Capacity:"></asp:Label>
                                            <asp:DropDownList ID="ddlSrchCash" runat="server" AutoPostBack="True"
                                                CssClass="ddlPage136 inputTxt"
                                                OnSelectedIndexChanged="ddlSrchCash_SelectedIndexChanged">
                                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                                <asp:ListItem Value="=">Equal</asp:ListItem>
                                                <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                                <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                                <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                                <asp:ListItem Value="&gt;=">Greater Then&nbsp; Equal</asp:ListItem>
                                                <asp:ListItem Value="between">Between</asp:ListItem>
                                            </asp:DropDownList>

                                            <asp:TextBox ID="txtAmountC1" runat="server" CssClass="inputTxt inpPixedWidth"></asp:TextBox>
                                            <asp:Label ID="lblToCash" runat="server" Text="To:" CssClass=" smLbl_to" Visible="False"></asp:Label>

                                            <asp:TextBox ID="txtAmountC2" runat="server" CssClass="inputTxt inpPixedWidth" Visible="False"></asp:TextBox>


                                        </div>
                                        <div class="col-md-1 pading5px">
                                        </div>
                                        <div class="col-md-1 pading5px">
                                        </div>


                                        <div class="clearfix"></div>


                                    </div>
                                </asp:Panel>

                            </div>
                        </fieldset>

                 
                            <asp:GridView ID="gvClientOff" runat="server" OnRowDataBound="gvClientOff_RowDataBound"
                                AutoGenerateColumns="False" PageSize="15" ShowFooter="true"
                                CssClass="table table-striped table-hover table-bordered grvContentarea" OnPageIndexChanging="gvClientOff_PageIndexChanging">
                                <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                    Mode="NumericFirstLast" />

                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Project Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvprojectName" runat="server" Text='<%# "<B>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))+"</B>" %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Unit Name">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFunitname" runat="server" Font-Bold="True" Font-Size="11px"
                                                >Total</asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgunitname" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit Size">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFusize" runat="server" Font-Size="11px" Font-Bold="true"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvusize" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <FooterStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvrate" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0;(#,##0); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvuamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFuamt" runat="server" Font-Size="11px"  Font-Bold="true"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />

                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Car Parking & Others">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvcpaothamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paothamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:Label ID="lgvFcparaothamt" runat="server" Font-Size="11px" Font-Bold="true"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtoamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tuamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:Label ID="lgvFtuamt" runat="server" Font-Size="11px" Font-Bold="true"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Facing">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvfacing" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "facing")) %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvview" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "uview")) %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Client Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgClName" runat="server" Text='<%# "<B>" + Convert.ToString(DataBinder.Eval(Container.DataItem, "prosdesc")) + "</B>" +
                                                                         (DataBinder.Eval(Container.DataItem, "phone").ToString().Trim().Length>0 ? "<br>"+
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")).Trim()  : "") 
                                                                      
                                                                         
                                                                    %>'
                                                Width="150px">
                                            
                                            
                                            
                                            </asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>





                                    <asp:TemplateField HeaderText="Offered Date">
                                        <ItemTemplate>
                                            <asp:Label ID="nappdat" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cdate")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Offered Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvoffrate" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "orate")).ToString("#,##0;(#,##0); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Offered Price">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvoffprice" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ouamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Parking & Others(Offered)">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvoffpaothers" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opaothamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Amount(Offered)">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvofftuamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "otuamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Diff in %">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvdiffinper" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dinper")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Booking">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvBooking" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bookamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Discusstion">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvdiscusstion" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "discus")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText=" Sales Team">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvsalesteam" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "teamdesc")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                </Columns>

                                <FooterStyle BackColor="#F5F5F5" />
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


        <script type="text/javascript">
            $(document).ready(function () {
                Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


            });
            function pageLoaded() {

           <%-- var gvClientOff = $('#<%=this.gvClientOff.ClientID %>');
            gvClientOff.Scrollable();--%>

                $('.chzn-select').chosen({ search_contains: true });

               

                $('#<%=this.gvClientOff.ClientID%>').gridviewScroll({
                    width: 1160,
                    height: 420,
                    arrowsize: 30,
                    railsize: 16,
                    barsize: 8,
                    varrowtopimg: "../Image/arrowvt.png",
                    varrowbottomimg: "../Image/arrowvb.png",
                    harrowleftimg: "../Image/arrowhl.png",
                    harrowrightimg: "../Image/arrowhr.png",
                    freezesize: 8


                });

                $("input, select").bind("keydown", function (event) {
                    var k1 = new KeyPress();
                    k1.textBoxHandler(event);

                });
            }

        </script>

</asp:Content>
