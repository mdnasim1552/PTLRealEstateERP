<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptInvResourceConsum.aspx.cs" Inherits="RealERPWEB.F_12_Inv.RptInvResourceConsum" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

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
                                        <asp:Label ID="lbldate1" runat="server" CssClass="lblTxt lblName" Text="Project Name"></asp:Label>
                                        <asp:TextBox ID="txtProjectSearch" runat="server" CssClass="inputtextbox"></asp:TextBox>

                                        <asp:LinkButton ID="ImgbtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ImgbtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>


                                    </div>

                                    <div class="col-md-5 pading5px ">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" AutoPostBack="true" CssClass="chzn-select form-control  inputTxt" style="width:336px;">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblProjectdesc" runat="server" Visible="False" CssClass="form-control inputTxt"></asp:Label>

                                    </div>
                                    <div class="col-md-2 pading5px">

                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click" Style="margin-left:-142px;">Ok</asp:LinkButton>



                                    </div>


                                </div>


                            </div>
                        </fieldset>
                    </div>
                    <asp:Panel ID="Panel2" runat="server" Visible="false">
                        <div class="row">

                            <fieldset class="scheduler-border fieldset_A">
                                <div class="form-horizontal">
                                    <div class="form-group">

                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblItem10" runat="server" CssClass="lblTxt lblName" Text="Floor"></asp:Label>
                                            <asp:DropDownList ID="ddlFloorListRpt" runat="server" AutoPostBack="true" CssClass=" ddlPage">
                                            </asp:DropDownList>
                                        </div>

                                        <div class="col-md-3 pading5px asitCol3 ">
                                            <asp:Label ID="lbldate2" runat="server" CssClass="lblTxt lblName">Material Name</asp:Label>
                                            <asp:TextBox ID="txtResSearch" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                            <asp:LinkButton ID="ImgbtnFindResource" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ImgbtnFindResource_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>


                                        </div>
                                        <div class="col-md-3 pading5px">
                                            <asp:DropDownList ID="ddlResource" runat="server" AutoPostBack="true" CssClass=" form-control inputTxt" >
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:LinkButton ID="lbtnShow" runat="server" OnClick="lbtnShow_Click" CssClass="btn btn-primary primaryBtn">Show</asp:LinkButton>
                                        </div>

                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName" Text="Page"></asp:Label>

                                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" TabIndex="18">
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
                                        <div class="col-md-6 pading5px">
                                            <asp:Label ID="lblRptGroup" runat="server" CssClass="lblTxt lblName" Text="Group"></asp:Label>


                                            <asp:DropDownList ID="ddlRptGroup" runat="server" CssClass="ddlPage">
                                                <asp:ListItem>Main</asp:ListItem>
                                                <asp:ListItem>Sub-1</asp:ListItem>
                                                <asp:ListItem>Sub-2</asp:ListItem>
                                                <asp:ListItem>Sub-3</asp:ListItem>
                                                <asp:ListItem Selected="True">Details</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:Label ID="lblRptGroup0" runat="server" CssClass="lblTxt smLbl_to" Text="Date"></asp:Label>
                                            <asp:TextBox ID="txtdate" runat="server" CssClass="inputtextbox" AutoCompleteType="Disabled"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server"
                                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>





                                        </div>

                                    </div>
                                </div>
                            </fieldset>

                            <%--<asp:Panel ID="Panel2" runat="server" Width="880px" BorderColor="Yellow" 
                             BorderStyle="Solid" BorderWidth="1px" Visible="False">
                             <table style="width:870px;">
                                 <tr>
                                     <td class="style28">
                                         <asp:Label ID="lblItem10" runat="server" CssClass="style27" Font-Size="12px" 
                                             Font-Underline="False" Height="16px" style="font-weight: 700; text-align:right" 
                                             Text="Floor :" Width="80px"></asp:Label>
                                     </td>
                                     <td class="style28">
                                         <asp:DropDownList ID="ddlFloorListRpt" runat="server" Font-Bold="True" 
                                             Font-Size="12px" Height="21px" style="text-transform: capitalize" Width="120px">
                                         </asp:DropDownList>
                                     </td>
                                     <td class="style19">
                                         <asp:Label ID="lbldate2" runat="server" CssClass="style27" 
                                             style="text-align: right; font-weight: 700;" Text="Material Name:" 
                                             Width="112px" Font-Bold="True" Font-Size="12px"></asp:Label>
                                     </td>
                                     <td class="style35">
                                         <asp:TextBox ID="txtResSearch" runat="server" BorderStyle="Solid" 
                                             BorderWidth="1px" Height="18px" Width="100px"></asp:TextBox>
                                     </td>
                                     <td class="style24">
                                         <asp:ImageButton ID="ImgbtnFindResource" runat="server" Height="19px" 
                                             ImageUrl="~/Image/find_images.jpg" 
                                             Width="16px" onclick="ImgbtnFindResource_Click" />
                                     </td>
                                     <td>
                                         <asp:DropDownList ID="ddlResource" runat="server" AutoPostBack="True" 
                                             CssClass="newStyle1" Font-Bold="True" Font-Size="11px" Height="21px" 
                                             Width="300px">
                                         </asp:DropDownList>
                                         <asp:LinkButton ID="lbtnShow" runat="server" BackColor="#003366" 
                                             BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                             Font-Size="12px" onclick="lbtnShow_Click" 
                                             style="text-align: center; color: #FFFFFF;" Width="50px">Show</asp:LinkButton>
                                     </td>
                                     <td>
                                         &nbsp;</td>
                                     <td>
                                         &nbsp;</td>
                                 </tr>
                                 <tr>
                                     <td class="style28">
                                         <asp:Label ID="lblPage" runat="server" CssClass="style27" Font-Size="12px" 
                                             Font-Underline="False" Height="16px" style="font-weight: 700; text-align:right" 
                                             Text="Page Size :" Width="80px" Visible="False"></asp:Label>
                                     </td>
                                     <td class="style28">
                                         <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" 
                                             BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" 
                                            Visible="False" 
                                             Width="120px" onselectedindexchanged="ddlpagesize_SelectedIndexChanged">
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
                                     </td>
                                     <td class="style19">
                                         <asp:Label ID="lblRptGroup" runat="server" CssClass="style27" Font-Size="12px" 
                                             Font-Underline="False" style="font-weight: 700; text-align:right" 
                                             Text="Group :" Width="112px"></asp:Label>
                                     </td>
                                     <td class="style35">
                                         <asp:DropDownList ID="ddlRptGroup" runat="server" Font-Bold="True" 
                                             Font-Size="12px" Height="21px" style="text-transform: capitalize" Width="100px">
                                             <asp:ListItem>Main</asp:ListItem>
                                             <asp:ListItem>Sub-1</asp:ListItem>
                                             <asp:ListItem>Sub-2</asp:ListItem>
                                             <asp:ListItem>Sub-3</asp:ListItem>
                                             <asp:ListItem Selected="True">Details</asp:ListItem>
                                         </asp:DropDownList>
                                     </td>
                                     <td class="style24">
                                         <asp:Label ID="lblRptGroup0" runat="server" CssClass="style27" Font-Size="12px" 
                                             Font-Underline="False" style="font-weight: 700; text-align:right" Text="Date:"></asp:Label>
                                     </td>
                                     <td>
                                         
                                     </td>
                                     <td>
                                         &nbsp;</td>
                                     <td>
                                         &nbsp;</td>
                                 </tr>
                             </table>
                         </asp:Panel>--%>
                        </div>
                    </asp:Panel>

                    <asp:GridView ID="gvRptResBasis" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        ShowFooter="True" Width="799px" AllowPaging="True" OnPageIndexChanging="gvRptResBasis_PageIndexChanging">
                        <PagerSettings Position="Top" />
                        <Columns>
                            <asp:TemplateField HeaderText="Floor">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvRptFlr1" runat="server" Font-Bold="False" Font-Size="12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                        Width="120px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="False" Font-Size="12px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField FooterText="Total" HeaderText="Resource Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvRptRes1" runat="server" Font-Bold="False" Font-Size="12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc")) %>'
                                        Width="300px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <FooterStyle Font-Bold="true" Font-Size="14px" HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Work Quantity">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvworktQty" runat="server" Font-Bold="False" Font-Size="12px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptbgdqty")).ToString("#,##0.00;(#,##0.00);-") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />

                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvRptUnit1" runat="server" Font-Bold="False" Font-Size="12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptunit")) %>'
                                        Width="30px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText=" Material Quantity">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvRptQty1" runat="server" Font-Bold="False" Font-Size="12px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptqty")).ToString("#,##0.00;(#,##0.00);-") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterTemplate>
                                    <asp:Label ID="lgvFqty" runat="server" Font-Bold="True" Font-Size="12px"
                                        Style="text-align: right" Width="80px"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rate">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvRptRat1" runat="server" Font-Bold="False" Font-Size="12px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptrat")).ToString("#,##0.00;(#,##0.00);-") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvRptAmt1" runat="server" Font-Bold="False" Font-Size="12px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptamt")).ToString("#,##0.00;(#,##0.00);-") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <FooterStyle HorizontalAlign="Right" />
                                <FooterTemplate>
                                    <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                        Style="text-align: right" Width="80px"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle Font-Bold="true" Font-Size="14px" HorizontalAlign="Right" />
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



            <table style="width: 77%;">
                <tr>
                    <td class="style17">
                        <%--<asp:Panel ID="Panel1" runat="server" Width="1052px">
                           
                            <table style="width:100%;">
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style30">
                                        <asp:Label ID="lbldate1" runat="server" CssClass="style27" 
                                            style="text-align: right; font-weight: 700;" Text="Project Name:" 
                                            Width="100px" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td class="style31">
                                        <asp:TextBox ID="txtProjectSearch" runat="server" BorderStyle="Solid" 
                                            BorderWidth="1px" Height="18px" Width="100px"></asp:TextBox>
                                    </td>
                                    <td class="style32">
                                        <asp:ImageButton ID="ImgbtnFindProject" runat="server" Height="19px" 
                                            ImageUrl="~/Image/find_images.jpg" onclick="ImgbtnFindProject_Click" 
                                            Width="16px" />
                                    </td>
                                    <td class="style33">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" AutoPostBack="True" 
                                            CssClass="newStyle1" Font-Bold="True" Font-Size="11px" Height="21px" 
                                            Width="300px">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblProjectdesc" runat="server" BackColor="White" 
                                            Font-Size="12px" ForeColor="Blue" Height="16px" Visible="False" Width="300px"></asp:Label>
                                        <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#003366" 
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" onclick="lbtnOk_Click" 
                                            style="text-align: center; color: #FFFFFF;" Width="50px">Ok</asp:LinkButton>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>
                           
                        </asp:Panel>--%>
                    </td>
                </tr>
            </table>
            <table style="width: 376px;">
                <tr>
                    <td class="style18" colspan="18"></td>
                </tr>
                <tr>
                    <td colspan="18" class="style18"></td>
                </tr>
                <tr>
                    <td class="style18">&nbsp;</td>
                    <td class="style43">&nbsp;</td>
                    <td class="style21">&nbsp;</td>
                    <td class="style20">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="style136">&nbsp;</td>
                    <td class="style152">&nbsp;</td>
                    <td class="style153">&nbsp;</td>
                    <td class="style125">&nbsp;</td>
                    <td class="style29">&nbsp;</td>
                    <td class="style123">&nbsp;</td>
                    <td class="style119">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="style43">&nbsp;</td>
                    <td class="style112">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

