<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="ProTargetTimeBasis.aspx.cs" Inherits="RealERPWEB.F_08_PPlan.ProTargetTimeBasis" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(PageLoad);

        });

        function PageLoad() {
            var gv = $('#<%=this.gvProTarget.ClientID %>');
            gv.Scrollable();

            $('.chzn-select').chosen({ search_contains: true });
        }
    </script>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
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
                                        <asp:Label ID="lblProjectList" runat="server" CssClass="lblTxt lblName">Project Name</asp:Label>
                                        <asp:TextBox ID="txtProjectSearch" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="ImgbtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="ImgbtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>

                                    <div class="col-md-5 pading5px ">
                                        <asp:DropDownList ID="ddlProject" runat="server" CssClass="chzn-select form-control inputTxt" AutoPostBack="True" OnSelectedIndexChanged="ddlProject_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblProjectDesc" runat="server" Visible="false" CssClass="form-control inputTxt dataLblview"> </asp:Label>


                                    </div>
                                    <div class="col-md-1 pading5px">

                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                        `
                                    </div>
                                    <div class="col-md-4">
                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                            <ProgressTemplate>
                                                <asp:Label ID="Label3" runat="server" CssClass="btn btn-warning pre-scrollable"
                                                    Text="Please wait . . . . . . ."></asp:Label>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </div>
                                </div>
                                <asp:Panel ID="PnlColoumn" runat="server" Visible="False">


                                    <div class="form-group">
                                        <div class="col-md-8 pading5px">
                                            <asp:Label ID="lbl01" runat="server" CssClass="lblTxt lblName">Start Date</asp:Label>
                                            <asp:TextBox ID="lblStartDate" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <asp:Label ID="lbl2" runat="server" CssClass=" smLbl_to">End Date</asp:Label>
                                            <asp:TextBox ID="lblEndDate" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>

                                            <asp:Label ID="lbl3" runat="server" CssClass="smLbl_to">Duration</asp:Label>
                                            <asp:TextBox ID="lblDuration" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>

                                            <asp:Label ID="lblPage" runat="server" CssClass=" smLbl_to">Page</asp:Label>
                                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" CssClass=" ddlPage" Style="width: 112px;">
                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                <asp:ListItem Value="20">20</asp:ListItem>
                                                <asp:ListItem Value="30">30</asp:ListItem>
                                                <asp:ListItem Value="50">50</asp:ListItem>
                                                <asp:ListItem Value="100">100</asp:ListItem>
                                                <asp:ListItem Value="150">150</asp:ListItem>
                                                <asp:ListItem Value="200">200</asp:ListItem>
                                                <asp:ListItem Value="300">300</asp:ListItem>
                                            </asp:DropDownList>

                                        </div>



                                        <div class="col-md-3 pading5px">


                                            <asp:CheckBox ID="chkWorkwise" runat="server" TabIndex="10" Text="Work Wise" CssClass="btn btn-primary checkBox" AutoPostBack="True" OnCheckedChanged="chkWorkwise_CheckedChanged" />
                                            <asp:RadioButtonList runat="server" ID="rbtndelayaext" CssClass="rbtnList1 chkBoxControl" RepeatDirection="Horizontal" Style="background-color: #DFF0D8; border-color: #000000;">
                                                <asp:ListItem Value="Delay" Selected="True">Delay</asp:ListItem>
                                                <asp:ListItem Value="Extend">Extend</asp:ListItem>
                                            </asp:RadioButtonList>



                                        </div>


                                        <div class="col-md-1">

                                            <asp:CheckBox ID="checkBalance" runat="server" TabIndex="10" Text="Balance" CssClass="btn btn-primary checkBox" AutoPostBack="True" OnCheckedChanged="checkBalance_CheckedChanged" />
                                        </div>

                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblsrchmaterial" runat="server" CssClass="lblTxt lblName">Search Item</asp:Label>
                                            <asp:TextBox ID="txtsrchmaterial" runat="server" CssClass=" inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <asp:LinkButton ID="lnksrchmaterial" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="lnksrchmaterial_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>


                                        <div class="col-md-4 pading5px asitCol4">

                                            <asp:Label ID="lblgrpwise" runat="server" CssClass="smLbl_to" Text="Group" Visible="false"></asp:Label>
                                            <asp:DropDownList ID="ddlgroupwise" runat="server" CssClass=" ddlPage chzn-select" TabIndex="12" AutoPostBack="True" OnSelectedIndexChanged="ddlgroupwise_SelectedIndexChanged" Visible="false" Style="width: 270px;">
                                            </asp:DropDownList>


                                        </div>

                                        <div class="col-md-3 pading5px">

                                            <asp:Label ID="lblfloorno" runat="server" CssClass="smLbl_to" Text="Catagory"></asp:Label>
                                            <asp:DropDownList ID="ddlfloorno" runat="server" CssClass=" ddlPage" Width="100" TabIndex="12" AutoPostBack="True" OnSelectedIndexChanged="ddlfloorno_SelectedIndexChanged">
                                            </asp:DropDownList>

                                        </div>




                                    </div>



                                </asp:Panel>
                                <asp:GridView ID="gvProTarget" runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="HeaderStyle"
                                    Width="16px" ShowFooter="True" OnPageIndexChanging="gvProTarget_PageIndexChanging" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    AllowPaging="True">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="False" Font-Size="12px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px">
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Catagory">

                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnAdddays" runat="server" CssClass="btn btn-primary primaryBtn"
                                                    OnClick="lbtnAdddays_Click">Add</asp:LinkButton>

                                                <asp:TextBox ID="txtadddays" runat="server" CssClass=" inputtextbox txtAlgRight" Style="width: 40px;"></asp:TextBox>

                                            </FooterTemplate>


                                            <ItemTemplate>
                                                <asp:Label ID="lblgvFloorName" runat="server" Font-Size="12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes"))   %>'
                                                    Width="90px"> 
                                                                        
                                                                         
                                                </asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Description ">

                                            <HeaderTemplate>
                                                <asp:Label ID="lblheader" runat="server" Font-Bold="True" Text="Description" Width="200px"></asp:Label>
                                                <asp:HyperLink ID="hlbtntbCdataExcel01" runat="server" CssClass="btn  btn-success btn-xs" ToolTip="Export Excel"><i  class=" fa fa-file-excel-o "></i>
                                                </asp:HyperLink>
                                            </HeaderTemplate>
                                            <%--  <HeaderTemplate>

                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblheader" runat="server" Text="Description"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:HyperLink ID="hlbtntbCdataExel" runat="server" CssClass="btn btn-danger btn-xs fa fa-file-excel-o" ToolTip="Export to Excel"></asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>--%>

                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnUpdate_Click">Final Update</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItemDesc" runat="server" Font-Size="12px" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "isirdesc")) 
                                                                         
                                                                         
                                                                    %>'
                                                    Width="230px">   </asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnTotal" runat="server" CssClass="btn btn-primary primaryBtn"
                                                    OnClick="lbtnTotal_Click">Total</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isirunit")) %>'
                                                    Width="35px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Budgeted Qty">

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvbgdqty" runat="server" CssClass="style101" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Start Date">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvstartdate" runat="server" CssClass="style101" Font-Size="11px"
                                                    Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "startdate")).ToString("dd-MMM-yyyy")=="01-Jan-1900")?""
                                            :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "startdate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtgvstartdate_CalendarExtender" runat="server" Enabled="True"
                                                    Format="dd-MMM-yyyy" TargetControlID="txtgvstartdate"></cc1:CalendarExtender>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="End Date">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvenddate" runat="server" CssClass="style101" Font-Size="11px"
                                                    Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "enddate")).ToString("dd-MMM-yyyy")=="01-Jan-1900")?""
                                            :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "enddate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtgvenddate_CalendarExtender" runat="server" Enabled="True"
                                                    Format="dd-MMM-yyyy" TargetControlID="txtgvenddate"></cc1:CalendarExtender>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <FooterStyle HorizontalAlign="Right" />

                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnPutSameValue" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnPutSameValue_Click">Same Value</asp:LinkButton>
                                            </FooterTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Day's">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvtodays" runat="server" CssClass="style101" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "today")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Budgeted Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvbgdrate" runat="server" CssClass="style101" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Budgeted Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvBgdAmt" runat="server" CssClass="style101" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFBgdAmt" runat="server" Font-Bold="true" ForeColor="#000" Font-Size="10px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Program Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvProAmt" runat="server" CssClass="style101" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFProAmt" runat="server" Font-Bold="true" ForeColor="#000" Font-Size="10px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Actual Start Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvacstdate" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "exstartdate")).ToString("dd-MMM-yyyy")=="01-Jan-1900"?"": Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "exstartdate")).ToString("dd-MMM-yyyy") %>'
                                                   
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Actual End Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvacenddate" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "exenddate")).ToString("dd-MMM-yyyy")=="01-Jan-1900"?"": Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "exenddate")).ToString("dd-MMM-yyyy") %>'
                                                     
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Ac. Total Day's">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvacdays" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "exdur")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
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
