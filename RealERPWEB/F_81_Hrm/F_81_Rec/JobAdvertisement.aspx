<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="JobAdvertisement.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_81_Rec.JobAdvertisement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="../../Scripts/jquery-1.4.1.min.js"></script>
    <script src="../../Scripts/jquery.keynavigation.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="../../Scripts/KeyPress.js"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
            var gridview = $('#<%=this.gvAdvInfo.ClientID %>');
            $.keynavigation(gridview);
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
            <div class="card mt-5">
               
                            <div class="card-header">
                                <div class="row">
                                    <div class="col-lg-2 col-md-2 col-ms-6">
                                        <div class="form-group">

                                            <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName">Company
                                                     <asp:LinkButton ID="ImgbtnFindCompName" runat="server" OnClick="ImgbtnFindComp_Click"><i class="fas fa-search"> </i></asp:LinkButton>
                                            </asp:Label>
                                            <asp:TextBox ID="txtCompSearch" runat="server" CssClass="form-control"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-ms-6 mt-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label1" Visible="false" runat="server" CssClass="lblTxt lblName">Company</asp:Label>
                                            <asp:DropDownList ID="ddlCompany" runat="server" Width="233" CssClass="form-control chzn-select" AutoPostBack="true" TabIndex="2">
                                            </asp:DropDownList>

                                            <asp:Label ID="lblddlCompany" runat="server" CssClass="dataLblview form-coltrol" Visible="False"></asp:Label>
                                        </div>

                                    </div>
                                    <div class="col-lg-1"></div>
                                    <div class="col-lg-3 col-md-3 col-ms-6">
                                        <div class="form-group">
                                            <asp:Label ID="lblCurDate" runat="server">Adv. Date</asp:Label>
                                            <asp:TextBox ID="txtCurAdvDate" runat="server" CssClass=" form-control "></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtCurAdvDate_CalendarExtender" runat="server" Format="dd.MM.yyyy" TargetControlID="txtCurAdvDate"></cc1:CalendarExtender>
                                        </div>
                                    </div>

                                    <div class="col-lg-1 col-md-1 col-ms-6 mt-3">
                                        <div class="form-group">
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn pull-left" OnClick="lbtnOk_Click">ok</asp:LinkButton>
                                            <asp:Label ID="lblLastReqNo4" runat="server" Text="" Width="80px"></asp:Label>
                                        </div>

                                    </div>


                                </div>
                                <div class="row">


                                    <div class="col-md-2 ">
                                        <div class="form-group">
                                           
                                                <asp:Label ID="lblCurNo" runat="server" CssClass="lblTxt lblName">ADV No
                                                     <asp:LinkButton ID="ImgbtnReqse" runat="server"  OnClick="ImgbtnReqse_Click"><span class="fas fa-search"> </span></asp:LinkButton>
                                                </asp:Label>
                                                <asp:TextBox ID="txtADVText" runat="server" CssClass="form-control"></asp:TextBox>
                                               
                                            </div>
                                        </div>
                                            <div class="col-md-5 ">
                                                <div class="form-group">
                                                <asp:Label ID="lblCurAdvNo1" runat="server" Font-Bold="True" CssClass="inputTxt inputName" Style="border: 1px solid #000000; padding: 1px 4px; text-align: right; background-color: #FFFFFF;"
                                                    Text="ADV00-"
                                                    Width="55px"></asp:Label>
                                                <asp:TextBox ID="txtCurAdvNo2" runat="server" BorderStyle="Solid" CssClass="inputTxt inputName" ReadOnly="True" Width="45px" TabIndex="7">00000</asp:TextBox>

                                                <asp:Label ID="lblAdvno" runat="server" CssClass="smLbl_to" Text="Ref No.:" Width="70px"></asp:Label>

                                                <asp:TextBox ID="txtMRFNo" runat="server" CssClass="form-control" TabIndex="8"></asp:TextBox>


                                                <asp:Label ID="lblSource" runat="server" CssClass=" smLbl_to " Text="Job Source:"></asp:Label>
                                                </div>
                                                </div>
                                    <div class="col-md-3">
                                        <div class="form-group">


                                            <asp:DropDownList ID="ddlSource" runat="server" AutoPostBack="True" Width="105" Style="padding: 0 2px;" CssClass="form-control chzn-select">
                                            </asp:DropDownList>
                                            <asp:Label ID="lblJobSource" runat="server" __designer:wfdid="w4" BackColor="White"
                                                Font-Bold="True" Font-Size="14px" ForeColor="Maroon" Style="font-size: 12px; text-align: left"
                                                Visible="False" Width="130px"></asp:Label>

                                            <asp:Label ID="lblprintstk" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                            </div>

                                     



                                <div class="row">
                
                                        <div class="form-group">
                                            <div class="col-md-3">
                                                <asp:Label ID="lblpreReq" runat="server" CssClass="lblTxt lblName">ADV List
                                                     <asp:LinkButton ID="ImgbtnFindReq" runat="server"  OnClick="ImgbtnFindReq_Click"><span class="fas fa-search"> </span></asp:LinkButton>
                                                </asp:Label>
                                                <asp:TextBox ID="txtSrchPre" runat="server" CssClass="form-control"></asp:TextBox>
                                               
                                            </div>
                                            <div class="col-md-3 ">

                                                <asp:DropDownList ID="ddlPrevAdvList" runat="server" Width="233" CssClass="form-control inputTxt pull-left" AutoPostBack="true" TabIndex="2">
                                                </asp:DropDownList>

                                                <asp:Label ID="lblmsg1" runat="server" CssClass=" btn btn-danger primaryBtn" Visible="false"></asp:Label>


                                            </div>
                                        </div>
                                    </div>

                                   </div>

                                     </div>

                            </div>



                                   
                                </div>



                                <div class="form-horizontal">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <div class="form-group">
                                            <div class="col-md-3 pading5px asitCol3">
                                                <asp:Label ID="Label3" runat="server" CssClass="lblTxt lblName">Department List</asp:Label>
                                                <asp:TextBox ID="txtDepList" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                                <asp:LinkButton ID="imgBtnDept" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgBtnDept_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                            </div>

                                            <div class="col-md-3 pading5px asitCol3">

                                                <asp:DropDownList ID="ddlDept" runat="server" Width="233" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="form-control inputTxt pull-left" AutoPostBack="true" TabIndex="2">
                                                </asp:DropDownList>

                                                <asp:Label ID="Label4" runat="server" CssClass="dataLblview" Visible="False" Width="233px"></asp:Label>


                                            </div>

                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-3 pading5px asitCol3">
                                                <asp:Label ID="lblResList" runat="server" CssClass="lblTxt lblName">Post List</asp:Label>
                                                <asp:TextBox ID="txtPostSearch" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                                <asp:LinkButton ID="ImgbtnFindPost" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ImgbtnFindPost_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                            </div>
                                            <div class="col-md-4 pading5px">

                                                <asp:DropDownList ID="ddlPOSTList" runat="server" Width="233" CssClass="form-control inputTxt pull-left"  TabIndex="2">
                                                </asp:DropDownList>

                                                <asp:LinkButton ID="lbtnSelectRes" runat="server" OnClick="lbtnSelectRes_Click" CssClass="btn btn-primary primaryBtn"
                                                    TabIndex="21">Select</asp:LinkButton>
                                            </div>

                                        </div>

                                    </asp:Panel>

                                </div>

                                <div class="card-body">
                                    <asp:Panel ID="Panel2" runat="server" Visible="false">
                                        <asp:GridView ID="gvAdvInfo" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                            OnRowDeleting="gvAdvInfo_RowDeleting" ShowFooter="True" PageSize="20" OnPageIndexChanging="gvAdvInfo_PageIndexChanging">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo0" runat="server" Height="16px" Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:CommandField ShowDeleteButton="True" />
                                                <asp:TemplateField HeaderText="Res Code" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvResCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postcode")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Spcf Code" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSpcfCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Department Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvDeptName" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptdesc")) %>'
                                                            Width="120px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description of Job Advertisement">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvResDesc" runat="server" Text='<%# "<B>" + Convert.ToString(DataBinder.Eval(Container.DataItem, "postdesc")) + "</B>" +
                                                                         (DataBinder.Eval(Container.DataItem, "gdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "postdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")).Trim(): "")   %>'
                                                            Width="220px">
                                                            
                                                            
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Requirements">
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lbtnUpdateResReq" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnUpdateResReq_Click" Style="text-align: center;">Final Update</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvRequ" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                            BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "requir").ToString() %>' Width="250px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                            <FooterStyle CssClass="grvFooter" />
                                            <AlternatingRowStyle BackColor="" />
                                        </asp:GridView>

                                        <%--<table style="width: 100%; height: 133px;">
                                        <tr>
                                            <td style="height: auto;" colspan="12" valign="top"></td>
                                        </tr>
                                        <tr>
                                            <td class="style71">
                                                <asp:Label ID="lblReqNarr" runat="server" CssClass="style15" Font-Bold="True" Font-Size="12px"
                                                    Height="16px" Style="text-align: right" Text="Narration:" Width="80px"></asp:Label>
                                            </td>
                                            <td class="style43" colspan="5">
                                                <asp:TextBox ID="txtAdvNarr" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Bold="True" Font-Size="12px" Height="40px" TextMode="MultiLine" Width="415px"></asp:TextBox>
                                            </td>
                                            <td align="left" valign="top" class="style83">&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td class="style53">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style71">
                                                <asp:Label ID="lblPreparedBy" runat="server" CssClass="style15" Font-Bold="True"
                                                    Font-Size="12px" Height="16px" Style="text-align: right" Text="Prepared By:"
                                                    Visible="False" Width="80px"></asp:Label>
                                            </td>
                                            <td class="style76">
                                                <asp:TextBox ID="txtPreparedBy" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Bold="True" Font-Size="12px" Visible="False" Width="90px"></asp:TextBox>
                                            </td>
                                            <td class="style74">&nbsp;
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblApprovedBy" runat="server" CssClass="style15" Font-Bold="True"
                                                    Font-Size="12px" Height="16px" Style="text-align: right" Text="Approved By:"
                                                    Visible="False" Width="80px"></asp:Label>
                                            </td>
                                            <td class="style54">
                                                <asp:TextBox ID="txtApprovedBy" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Bold="True" Font-Size="12px" Visible="False" Width="120px"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right" class="style81">
                                                <asp:Label ID="lblApprovalDate" runat="server" CssClass="style15" Font-Bold="True"
                                                    Font-Size="12px" Height="16px" Style="text-align: right" Text="Approv.Date:"
                                                    Visible="False" Width="65px"></asp:Label>
                                            </td>
                                            <td class="style83">
                                                <asp:TextBox ID="txtApprovalDate" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Bold="True" Font-Size="12px" ToolTip="(dd.mm.yyyy)" Visible="False" Width="100px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblExpDeliveryDate0" runat="server" CssClass="style15" Font-Bold="True"
                                                    Font-Size="12px" Height="16px" Style="text-align: right" Text="Exp.Del. Date:"
                                                    Visible="False" Width="80px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtExpDeliveryDate" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Bold="True" Font-Size="12px" ToolTip="(dd.mm.yyyy)" Visible="False" Width="90px"></asp:TextBox>
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td class="style53">&nbsp;
                                            </td>
                                        </tr>
                                    </table>--%>
                                        <div class="form-group">
                                            <div class="col-md-6 pading5px">
                                                <div class="input-group">
                                                    <span class="input-group-addon glypingraddon">
                                                        <asp:Label ID="lblReqNarr" runat="server" CssClass="lblTxt lblName" Text="Narration:"></asp:Label>
                                                    </span>
                                                    <asp:TextBox ID="txtAdvNarr" runat="server" class="form-control" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-2 pading5px asitCol2">
                                                <asp:Label ID="lblPreparedBy" runat="server" Visible="False"></asp:Label>

                                                <asp:TextBox ID="txtPreparedBy" runat="server" class="form-control" Visible="false"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2 pading5px asitCol2">
                                                <asp:Label ID="lblApprovedBy" runat="server" Visible="False"></asp:Label>

                                                <asp:TextBox ID="txtApprovedBy" runat="server" class="form-control" Visible="false"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2 pading5px asitCol2">
                                                <asp:Label ID="lblApprovalDate" runat="server" Text="Approv.Date:"
                                                    Visible="False" Width="65px"></asp:Label>

                                                <asp:TextBox ID="txtApprovalDate" runat="server" class="form-control" ToolTip="(dd.mm.yyyy)" Visible="False"></asp:TextBox>
                                                <asp:Label ID="lblExpDeliveryDate0" runat="server" Text="Exp.Del. Date:"
                                                    Visible="False" Width="80px"></asp:Label>

                                                <asp:TextBox ID="txtExpDeliveryDate" runat="server" ToolTip="(dd.mm.yyyy)" Visible="False"></asp:TextBox>
                                            </div>

                                            </td>
                                        </div>
                                    </asp:Panel>
                                </div>

                         
                        </div>
                    

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

