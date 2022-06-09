<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptMonthWiseTax02.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_89_Pay.RptMonthWiseTax02" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        div#ContentPlaceHolder1_ddlCompanyAgg_chzn{
        width:100%!important;
        }
        div#ContentPlaceHolder1_ddldepartmentagg_chzn{
            width:100%!important;
        }
        div#ContentPlaceHolder1_ddlProjectName_chzn{
             width:100%!important;
        }
        div#ContentPlaceHolder1_ddlEmployee_chzn{
              width:100%!important;
        }
        .mt20{
            margin-top:20px;
        }
        .chzn-drop{
            width:100%!important;
        }
                        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }
                .card-body{
                    min-height:400px!important;
                }
                        .pd4{
                    padding:4px!important;
                }

    </style>

    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {

              <%--  var gvpf = $('#<%=this.gvsalary.ClientID %>');


            gvpf.gridviewScroll({
                width: 1160,
                height: 420,
                arrowsize: 30,
                railsize: 16,
                barsize: 8,
                varrowtopimg: "../../Image/arrowvt.png",
                varrowbottomimg: "../../Image/arrowvb.png",
                harrowleftimg: "../../Image/arrowhl.png",
                harrowrightimg: "../../Image/arrowhr.png",
                freezesize: 10
            });--%>


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

            <div class="card mt-5">
                <div class="card-header">
                    <div class="row">
                        <div class="col-lg-3 col-md-3  col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="Label6" runat="server">Company</asp:Label>
                                <asp:DropDownList ID="ddlCompanyAgg" runat="server" CssClass="form-control chzn-select w-100" OnSelectedIndexChanged="ddlCompanyAgg_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3  col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="lbldeptnameagg" runat="server">Department</asp:Label>
                                <asp:DropDownList ID="ddldepartmentagg" runat="server" CssClass="form-control  chzn-select" TabIndex="7" AutoPostBack="True" OnSelectedIndexChanged="ddldepartmentagg_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>


                        <div class="col-lg-3 col-md-3 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="lblsection" runat="server">Section</asp:Label>
                                <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control chzn-select" AutoPostBack="True" TabIndex="7" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="lblpreAdv" runat="server">Employee</asp:Label>
                                <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control chzn-select" AutoPostBack="True" TabIndex="7" >
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-1 col-md-2 col-sm-6">
                            <asp:Label ID="lblfrmdate" runat="server">From</asp:Label>
                            <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control form-control-sm pd4"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>



                        </div>

                        <div class="col-lg-1 col-md-2 col-sm-6">
                            <asp:Label ID="lbltodate" runat="server">To</asp:Label>
                            <asp:TextBox ID="txttodate" runat="server" CssClass=" form-control form-control-sm pd4"></asp:TextBox>
                            <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                        </div>

                        <div class="col-lg-1 col-md-1 col-sm-2">
                                   <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm mt20" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                        </div>
                    </div>

                    
                </div>
                <div class="card-body">
                   <asp:GridView ID="gvsalary" runat="server" AutoGenerateColumns="False"
                    ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvsalary_RowDataBound">
                    <RowStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="Sl.No.">
                            <ItemTemplate>
                                <asp:Label ID="lblgvSlNo5" runat="server" Font-Bold="True" Height="16px"
                                    Style="text-align: right"
                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                           

                           <asp:TemplateField FooterText="Total"
                                                    HeaderText="Employee Name">
                                                    <HeaderTemplate>

                                                         <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                                        Text="Description Of Accounts" Width="180px"></asp:Label>


                                                        <asp:HyperLink ID="hlbtntbCdataExel11" runat="server"
                                                                        CssClass="btn  btn-success  btn-xs" ToolTip="Export Excel"><span class="fa  fa-file-excel "></span></asp:HyperLink>
                                                     
                                                    </HeaderTemplate>
                                                  <ItemTemplate>
                                <asp:Label ID="lgvsalempname" runat="server"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                    Width="200px"></asp:Label>
                            </ItemTemplate>
                                                 

                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="left" />
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                </asp:TemplateField>

                    <%--    <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                                <table style="width: 200px">
                                    <tr>
                                        <td class="style225">
                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" Height="16px"
                                                Text=" Employee Name " Width="90px"></asp:Label>
                                        </td>
                                        <td class="style237">
                                            <asp:HyperLink ID="hlbtntbCdataExel11" runat="server" BackColor="#000066"
                                                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                ForeColor="White" Style="text-align: center" Width="80px">Export Exel</asp:HyperLink>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lgvsalempname" runat="server"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                    Width="200px"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>--%>
                        
                        <asp:TemplateField HeaderText="Empid" visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lgvempid" runat="server"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                    Width="70px"></asp:Label>
                            </ItemTemplate>

                              <FooterTemplate>
                                <asp:Label ID="lgvFempid" runat="server" Font-Bold="True" Font-Size="12px" Width="70px"
                                    Style="text-align: left"></asp:Label>
                            </FooterTemplate>

                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="grp" visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lgvgrp" runat="server"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grp")) %>'
                                    Width="70px"></asp:Label>
                            </ItemTemplate>

                              <FooterTemplate>
                                <asp:Label ID="lgvFgrp" runat="server" Font-Bold="True" Font-Size="12px" Width="70px"
                                    Style="text-align: left"></asp:Label>
                            </FooterTemplate>

                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Code #">
                            <ItemTemplate>
                                <asp:Label ID="lgvsalcardno" runat="server"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                    Width="70px"></asp:Label>
                            </ItemTemplate>

                              <FooterTemplate>
                                <asp:Label ID="lgvFsalcardno" runat="server" Font-Bold="True" Font-Size="12px" Width="70px"
                                    Style="text-align: left"></asp:Label>
                            </FooterTemplate>

                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Tin">
                            <ItemTemplate>
                                <asp:Label ID="lgvtin" runat="server" Style="text-align: left"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tin"))%>'
                                    Width="120px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lgvFtin" runat="server" Font-Bold="True" Font-Size="12px" Width="120px"
                                    Style="text-align: left"></asp:Label>
                            </FooterTemplate>

                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                      

                        <asp:TemplateField HeaderText="Month">
                            <ItemTemplate>
                                <asp:Label ID="lgvmonthid" runat="server" Style="text-align: left"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "monthid1"))%>'
                                    Width="100px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lgvFmonthid" runat="server" Font-Bold="True" Font-Size="12px"
                                    Style="text-align: left"></asp:Label>
                            </FooterTemplate>

                            <ItemStyle HorizontalAlign="Left" />
                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tax">
                            <ItemTemplate>
                                <asp:Label ID="lgvtax" runat="server" Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itax")).ToString("#,##0;(#,##0); ") %>'
                                    Width="80px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lgvFtax" runat="server" Font-Bold="True" Font-Size="12px"
                                    Style="text-align: right"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                                                                                                                

                    </Columns>
                    <FooterStyle CssClass="grvFooter" />
                    <EditRowStyle />
                    <AlternatingRowStyle />
                    <PagerStyle CssClass="gvPagination" />
                    <HeaderStyle CssClass="grvHeader" />
                </asp:GridView>
                </div>




                <%--   <div class=" table table-responsive">--%>

   


            </div>




            <script type="text/javascript" language="javascript">

                $(document).ready(function () {

                    Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

                });

                function pageLoaded() {


                    $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
                    var gvsalary = $('#<%=this.gvsalary.ClientID %>');

                    //gvsalary.Scrollable();
                }

            </script>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

