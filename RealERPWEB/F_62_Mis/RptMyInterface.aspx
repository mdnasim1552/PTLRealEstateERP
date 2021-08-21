<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptMyInterface.aspx.cs" Inherits="RealERPWEB.F_62_Mis.RptMyInterface" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .headPart {
            font-weight: bold;
        }
    </style>
  
    <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">--%>
    <%-- <ContentTemplate>--%>
    <div class="container moduleItemWrpper">
        <div class="contentPart">

            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="ViewServices" runat="server">

                    <asp:Panel ID="Panel1" runat="server">

                        <div>
                            <fieldset class="scheduler-border fieldset_A">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Lasbel2" runat="server" CssClass="lblTxt lblName">Company</asp:Label>
                                            <asp:TextBox ID="txtSrcCompany" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <asp:LinkButton ID="imgbtnCompany" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnCompany_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                        <div class="col-md-4 pading5px">
                                            <asp:DropDownList ID="ddlCompany" runat="server" CssClass=" form-control inputTxt pull-left" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                            </asp:DropDownList>
                                            <asp:Label ID="lblCompanyName" runat="server" Width="233" CssClass="dataLblview" Visible="False"></asp:Label>
                                           
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Employee List:</asp:Label>
                                            <asp:TextBox ID="txtEmpSrc" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <asp:LinkButton ID="ibtnEmpList" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="ibtnEmpList_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                        <div class="col-md-4 pading5px">
                                            <asp:DropDownList ID="ddlEmpName" runat="server" AutoPostBack="false" CssClass=" form-control inputTxt" TabIndex="2">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3 pading5px">

                                            <asp:Label ID="lblEmpname" runat="server" Visible="false" CssClass=" smLbl_to"></asp:Label>

                                            <asp:Label ID="Label17" runat="server" CssClass=" smLbl_to"> Date</asp:Label>
                                            <asp:TextBox ID="txtDate" runat="server" CssClass="inputTxt inputName inPixedWidth120 " ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                                Enabled="True" Format="dd.MM.yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>

                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                        </div>

                                    </div>
                                </div>
                            </fieldset>
                        </div>


                    </asp:Panel>
                    <fieldset>
                        <div class="form-horizontal">

                            <div class="form-group">
                                <asp:Label ID="lblServHead" Visible="false" runat="server" CssClass="btn btn-success btn-sm headPart">SERVICE HISTORY</asp:Label>

                            </div>
                        </div>
                    </fieldset>
                    <div class="row">
                        <div class="col-md-9">
                            <div class="table-responsive">

                                <asp:GridView ID="gvempservices" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea" Width="678px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="serialno" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdescription" runat="server" Font-Size="11PX"
                                                    Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "descrip")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDate" runat="server" Font-Size="11PX"
                                                    Style="text-align: left"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "date")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Company">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvComp" runat="server" Font-Size="11PX"
                                                    Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Section">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSection" runat="server" Font-Size="11PX"
                                                    Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Increment">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvIncSalary" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "incrsal")).ToString("#, ##0;(#, ##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Salary">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSalary" runat="server" Font-Size="11PX"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tosalary")).ToString("#, ##0;(#, ##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvpredesig" runat="server" Font-Size="11PX"
                                                    Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>

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
                        <div class="col-md-3">
                            <p>
                                <asp:Image ID="EmpUserImg" runat="server" Visible="false" ImageUrl="~/Image/empImg.png" Height="105" Width="105" CssClass="img-thumbnail img-responsive" /></p>

                            <asp:LinkButton ID="hyplPreviewCv" CssClass="btn btn-success  btn-circle" Visible="false" runat="server" OnClick="hyplPreviewCv_Click1"> View Profile <i class="fa fa-search fa-spin"></i> </asp:LinkButton>


                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="table-responsive">

                                <fieldset>
                                    <div class="form-horizontal">

                                        <div class="form-group">
                                            <asp:Label ID="lbAttHead" runat="server" Visible="false" CssClass="btn btn-success btn-sm headPart">ATTENDANCE HISTORY </asp:Label>

                                        </div>
                                    </div>
                                </fieldset>
                                <asp:Repeater ID="RptAttHistroy" runat="server" OnItemDataBound="RptAttHistroy_ItemDataBound">
                                    <HeaderTemplate>
                                        <table class="table-striped table-hover table-bordered grvContentarea grvHeader grvFooter" style="width: 350px;">
                                            <tr>
                                                <th>Month </th>
                                                <th>Intime</th>
                                                <th>Late </th>
                                                <th>Absent </th>
                                                <th>Leave </th>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td style="width: 80px">
                                                <asp:Label ID="lblymonid" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ymonid")).ToString() %>'></asp:Label>
                                              
                                            <asp:HyperLink ID="hlnkbtnadd" runat="server"  Target="_blank" Text='<%# Eval("yearmon") %>'></asp:HyperLink>
                                                
                                            </td>
                                            <td style="width: 80px; text-align: right !important;">
                                                <asp:Label ID="lblacintime" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acintime")).ToString("#, ##0;(#, ##0); ") %>'></asp:Label>
                                            </td>

                                            <td style="width: 80px; text-align: right !important;">
                                                <asp:Label ID="lblLate" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aclate")).ToString("#, ##0;(#, ##0); ")%>'></asp:Label>
                                            </td>
                                            <td style="width: 80px; text-align: right !important;">
                                                <asp:Label ID="lblAbsent" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "absnt")).ToString("#, ##0;(#, ##0); ") %>'></asp:Label>
                                            </td>
                                            <td style="width: 80px; text-align: right !important;">
                                                <asp:Label ID="lblLeave" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "leave")).ToString("#, ##0;(#, ##0); ")%>'></asp:Label>
                                            </td>

                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <tr>
                                            <td style="width: 80px">
                                                <asp:Label ID="ttl" runat="server" CssClass=" smLbl_to" Text="Total"></asp:Label></td>
                                            <td style="width: 80px; text-align: right !important;">
                                                <asp:Label ID="lblacintime" runat="server" Style="text-align: right"></asp:Label>
                                            </td>
                                            <td style="width: 80px; text-align: right !important;">
                                                <asp:Label ID="lbltotallate" runat="server" Style="text-align: right"></asp:Label></td>
                                            <td style="width: 80px; text-align: right !important;">
                                                <asp:Label ID="lbltotalabs" runat="server" Style="text-align: right"></asp:Label>
                                            </td>

                                            <td style="width: 80px; text-align: right !important;">
                                                <asp:Label ID="lbltotalleave" runat="server" Style="text-align: right"></asp:Label></td>

                                        </tr>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>

                            </div>
                        </div>
                        <div class="col-md-8">
                            <fieldset>
                                <div class="form-horizontal">

                                    <div class="form-group">
                                        <asp:Label ID="lblgraph" runat="server" Visible="false" CssClass="btn btn-success btn-sm headPart">GRAPH</asp:Label>

                                    </div>
                                </div>
                            </fieldset>
                            <div>
                                <asp:Chart ID="AttHistoryGraph" runat="server" Visible="false" Width="750px" Height="300px">
                                    <Series>
                                        <asp:Series ChartArea="ChartArea1" Color="#008000" IsValueShownAsLabel="true"
                                            MarkerColor="black" MarkerStyle="None" Name="Actual in Time" MarkerSize="4" YValuesPerPoint="6">
                                        </asp:Series>
                                        <asp:Series ChartArea="ChartArea1" Color="#029ACF" IsValueShownAsLabel="true"
                                            MarkerColor="black" MarkerStyle="None" Name="Late" MarkerSize="4" YValuesPerPoint="6">
                                        </asp:Series>
                                        <asp:Series ChartArea="ChartArea1" Color="#FF2A2D" IsValueShownAsLabel="true"
                                            MarkerColor="black" MarkerStyle="None" Name="Absent" MarkerSize="4" YValuesPerPoint="6">
                                        </asp:Series>
                                        <asp:Series ChartArea="ChartArea1" Color="#4E69A2" IsValueShownAsLabel="true"
                                            MarkerColor="black" MarkerStyle="None" Name="Leave" MarkerSize="4" YValuesPerPoint="6">
                                        </asp:Series>
                                    </Series>
                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1">
                                            <AxisX MaximumAutoSize="100" Interval="1">
                                                <MajorGrid Enabled="False" />

                                            </AxisX>
                                            <AxisY>
                                                <MajorGrid Enabled="False" />
                                            </AxisY>

                                        </asp:ChartArea>
                                    </ChartAreas>
                                    <Legends>
                                        <asp:Legend></asp:Legend>
                                    </Legends>
                                </asp:Chart>

                            </div>
                        </div>
                    </div>
                    <fieldset>
                        <div class="form-horizontal">

                            <div class="form-group">
                                <asp:Label ID="lblleaveHis" runat="server" Visible="false" CssClass="btn btn-success btn-sm headPart">LEAVE HISTORY</asp:Label>

                            </div>
                        </div>
                    </fieldset>

                    <asp:GridView ID="gvLeaveStatus" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                        Width="600px">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Desription">
                                <HeaderTemplate>
                                    <asp:Label ID="lblgvDescr" runat="server" Text='Desription'
                                       ></asp:Label>

                                     <asp:HyperLink ID="hlnkbtnNext" runat="server" NavigateUrl="#" Target="_blank"  CssClass="btn btn-primary primaryBtn pull-right" Text="Next">

                                    </asp:HyperLink>
                                </HeaderTemplate>
                                <ItemTemplate>

                                    <asp:Label ID="lblgvDescription0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                        Width="120px"></asp:Label>
                                   

                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Entitlement">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvlentitled0" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "permonth")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Availed">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvlentitled01" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ltaken")).ToString("#,##0;(#,##0); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Balance">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvlentitled1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pbal")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="right" />
                            </asp:TemplateField>

                            <%--<asp:TemplateField HeaderText="Requested">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvballeave1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "applyday")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>--%>
                            <%--<asp:TemplateField HeaderText="Approved">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvballeave1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "appday")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>--%>
                            <%--<asp:TemplateField HeaderText="Closing Bal.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvballeave1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balleave")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>--%>
                            <%--<asp:TemplateField HeaderText="Last Leave Std. Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvenjoydt10" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt1")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px" Visible='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt1")).ToString("dd-MMM-yyyy")!="01-Jan-1900" %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Last Leave End Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvleavedt20" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt2")).ToString("dd-MMM-yyyy ") %>'
                                        Width="80px" Visible='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt2")).ToString("dd-MMM-yyyy")!="01-Jan-1900" %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Last Leave Day's">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvenjoyday0" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lenjoyday")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="right" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="grvFooter" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                    </asp:GridView>


                    <fieldset>
                        <div class="form-horizontal">

                            <div class="form-group">
                                <asp:Label ID="Lbljobres" runat="server" Visible="false" CssClass="btn btn-success btn-sm headPart">JOB RESPONSIBILITIES</asp:Label>

                            </div>
                        </div>
                    </fieldset>

                    <fieldset>
                        <div class="form-horizontal">

                            <div class="form-group">
                                <asp:Label ID="lblnotfound" runat="server" Visible="false" CssClass="headPart">No Result Found</asp:Label>

                            </div>
                        </div>
                    </fieldset>


                    <asp:GridView ID="grvJobRespo" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                        ShowFooter="True" Width="400px">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo42" runat="server" Font-Bold="True" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvItmCode1" runat="server" Height="16px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                        Width="49px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Job Responsibilities">
                                <ItemTemplate>

                                    <asp:Label ID="lgvgval1Job" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobresp")) %>'></asp:Label>

                                </ItemTemplate>



                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Type" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lgvgval1" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                        <FooterStyle CssClass="grvFooter" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                    </asp:GridView>

                </asp:View>









            </asp:MultiView>

        </div>
    </div>
    <%--</ContentTemplate>--%>
    <%-- </asp:UpdatePanel>--%>

    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $('.chzn-select').chosen({ search_contains: true });

        }

    </script>
</asp:Content>

