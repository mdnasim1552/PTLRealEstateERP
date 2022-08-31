<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="EnvelopeTemplate.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_97_MIS.EnvelopeTemplate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script src="../../../Scripts/gridviewScrollHaVertworow.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function printEnvelop(type) {
            window.open('../../RDLCViewerWin.aspx?PrintOpt=' + type, '_blank');
        }
        function pageLoaded() {

            try {

                var gridViewScroll = new GridViewScroll({
                    elementID: "gvpayroll",
                    width: 1400,
                    height: 500,
                    freezeColumn: true,
                    freezeFooter: true,
                    freezeColumnCssClass: "GridViewScrollItemFreeze",
                    freezeFooterCssClass: "GridViewScrollFooterFreeze",
                    freezeHeaderRowCount: 1,
                    freezeColumnCount: 8,

                });

                //var gridViewScroll = new GridViewScroll({
                //    elementID: "gvBonus",
                //    width: 1000,
                //    height: 500,
                //    freezeColumn: true,
                //    freezeFooter: true,
                //    freezeColumnCssClass: "GridViewScrollItemFreeze",
                //    freezeFooterCssClass: "GridViewScrollFooterFreeze",
                //    freezeHeaderRowCount: 1,
                //    freezeColumnCount: 8,

                //});
                gridViewScroll.enhance();
                $('.chzn-select').chosen({ search_contains: true });
            }

            catch (e) {
                alert(e);
            }
        }

    </script>
    <style>
        .GridViewScrollHeader TH, .GridViewScrollHeader TD {
            font-weight: normal;
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #F4F4F4;
            color: #999999;
            text-align: left;
            vertical-align: bottom;
        }

        .GridViewScrollItem TD {
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #FFFFFF;
            color: #444444;
        }

        .GridViewScrollItemFreeze TD {
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #FAFAFA;
            color: #444444;
        }

        .GridViewScrollFooterFreeze TD {
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-top: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #F4F4F4;
            color: #444444;
        }

        .grvHeader {
            height: 58px !important;
        }

        .WrpTxt {
            white-space: normal !important;
            word-break: break-word !important;
        }
    </style>


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

            <div class="card mt-5">
                <div class="card-header">
                    <div class="row">
                        <div class="col-lg-2 col-md-2 col-sm-6">
                            <asp:Label ID="Label8" runat="server">Company</asp:Label>
                            <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control chzn-select " OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6" id="divBracnhLsit" runat="server">
                            <asp:Label ID="Label9" runat="server">Branch</asp:Label>
                            <asp:DropDownList ID="ddlBranch" runat="server" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control chzn-select" TabIndex="2"></asp:DropDownList>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6">
                            <asp:Label ID="Label10" runat="server">Department</asp:Label>
                            <asp:DropDownList ID="ddlProjectName" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" AutoPostBack="True" runat="server" CssClass="form-control chzn-select" TabIndex="6">
                            </asp:DropDownList>

                            <%-- <cc1:ListSearchExtender  runat="server"
                                QueryPattern="Contains" TargetControlID="ddlProjectName">
                            </cc1:ListSearchExtender>--%>
                            <asp:Label ID="lblComBonLock" runat="server" CssClass="form-control inputTxt" Visible="False" Width="233"></asp:Label>
                            <asp:Label ID="lblComSalLock" runat="server" CssClass="form-control inputTxt" Visible="False" Width="233"></asp:Label>
                        </div>

                        <div class="col-lg-2 col-md-3 col-sm-6">
                            <asp:Label ID="Label11" runat="server">Section</asp:Label>
                            <asp:DropDownList ID="ddlSection" runat="server" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" CssClass="form-control chzn-select" TabIndex="6" AutoPostBack="true">
                            </asp:DropDownList>

                            <%-- <cc1:ListSearchExtender ID="ddlSection_ListSearchExtender" runat="server"
                                QueryPattern="Contains" TargetControlID="ddlSection">
                            </cc1:ListSearchExtender>--%>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6">

                            <asp:Label ID="lblemp" runat="server">Employee List                                  
                            <asp:LinkButton ID="ibtnEmpListAllinfo" runat="server" OnClick="ibtnEmpListAllinfo_Click"><i class="fa fa-search"> </i></asp:LinkButton>
                            </asp:Label>
                            <asp:DropDownList ID="ddlEmpNameAllInfo" runat="server" OnSelectedIndexChanged="ddlEmpNameAllInfo_SelectedIndexChanged" CssClass="form-control chzn-select" TabIndex="2" AutoPostBack="True">
                            </asp:DropDownList>

                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6">
                            <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary okBtn pull-left mt-4" OnClick="lnkbtnShow_Click">Ok</asp:LinkButton>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-lg-2 col-md-3 col-sm-6">

                            <asp:Label ID="lblTypeHeader" runat="server" CssClass="lblTxt lblName" Text="Envelope Type"></asp:Label>
                            <asp:DropDownList ID="ddlTypeHeader" runat="server" CssClass="form-control chzn-select" Font-Bold="true">
                                <%--                               <asp:ListItem Value="1">BY HAND</asp:ListItem>
                                            <asp:ListItem Value="2">REGISTER MAIL </asp:ListItem>
                                            <asp:ListItem Value="3">BY COURIER </asp:ListItem>
                                            <asp:ListItem Value="4">REGISTER MAIL WITH A/D</asp:ListItem>--%>
                            </asp:DropDownList>

                        </div>




                    </div>

                </div>
                <div class="card-body">

                    <div class="table table-responsive">
                        <asp:GridView ID="gvEmpList" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea"
                            AutoGenerateColumns="False" OnPageIndexChanging="gvEmpList_PageIndexChanging" AllowPaging="true"
                            ShowFooter="True" PageSize="300">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Card#">
                                    <HeaderTemplate>
                                        <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                            Text="Card"></asp:Label>
                                        <asp:HyperLink ID="hlbtntbCdataExcelemplist" runat="server"
                                            CssClass="btn  btn-success btn-sm" ToolTip="Export Excel"><i class="fa fa-file-excel"></i></asp:HyperLink>
                                    </HeaderTemplate>
                                    <ItemTemplate>

                                        <asp:Label ID="lblgvcardnoemp" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvdeptandemployeeemp" runat="server"
                                            Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "empname").ToString().Trim().Length>0 ? Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")).Trim(): "")  %>'
                                            Width="150px"> 
                                              
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="left" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvdesignationemp" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Department Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvdepname" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Joining Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvjoindateemp" runat="server" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joindate")).ToString("dd-MMM-yyyy") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Blood Group">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvbloodgrp" runat="server" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "blood")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Mobile">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvmobile" runat="server" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mobile")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Email">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmail" runat="server" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "email")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                </asp:TemplateField>




                                <asp:TemplateField HeaderText="Service Length" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvserlength" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slength")) %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Action">
                                     <HeaderTemplate>
                                          <asp:CheckBox ID="chkAllfrm" runat="server" AutoPostBack="True"
                                                            OnCheckedChanged="chkAllfrm_CheckedChanged" Text="ALL " />
                                     </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="isPrint" runat="server" Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isPrint"))=="True" %>' />
                                        
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true" Font-size="16px" />
                                    <ItemStyle HorizontalAlign="center" />
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

        </ContentTemplate>

    </asp:UpdatePanel>

</asp:Content>
