<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkMyHRLeave.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_82_App.LinkMyHRLeave" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .headPart {
            font-weight: bold;
            font-size:14PX;
            
        }

        .lblStyle {
            /*width: 70%;*/
        }
        .lblStyle2{
            width:73px;
            background: #fdfdfd none repeat scroll 0 0;
    border: 1px solid #ccc;
    border-radius: 2px;
    color: #000;
        }
        .lblName2 {
            width: 140px;
        }
    </style>


    <script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.4.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="../../Scripts/ScrollableGridPlugin.js"></script>
    <script type="text/javascript" language="javascript" src="../../Scripts/KeyPress.js"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });
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
                    <div>
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">

                                <div class="row">
                                    <div class="col-md-4">

                                        <div class="form-group">
                                            <div class="row">
                                                <asp:Label ID="Label11" runat="server" CssClass=" lblTxt lblName"> Card No</asp:Label>
                                                <asp:Label ID="lblcard" runat="server" CssClass=" smLbl_to lblStyle"> Card</asp:Label>
                                            </div>
                                            <div class="row">
                                                <asp:Label ID="Label5" runat="server" CssClass=" lblTxt lblName"> Emp Name</asp:Label>
                                                <asp:Label ID="lblname" runat="server" CssClass=" smLbl_to lblStyle"> Name</asp:Label>
                                            </div>

                                            <div class="row">
                                                <asp:Label ID="Label6" runat="server" CssClass=" lblTxt lblName">Desgnation</asp:Label>
                                                <asp:Label ID="lbldesg" runat="server" CssClass=" smLbl_to lblStyle"> Desgnation name</asp:Label>
                                            </div>
                                            <div class="row">
                                                <asp:Label ID="Label8" runat="server" CssClass=" lblTxt lblName">Department</asp:Label>
                                                <asp:Label ID="lbldpt" runat="server" CssClass=" smLbl_to lblStyle"> Department name</asp:Label>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="col-md-5" style="display:none">
                                        <div class="form-group">

                                            <div class="row">
                                                <asp:Label ID="Label17" runat="server" CssClass=" lblTxt lblName lblName2">Date of Joining</asp:Label>
                                                <asp:Label ID="lblLeave" runat="server" CssClass=" smLbl_to lblStyle"> DateofJon</asp:Label>
                                            </div>
                                            <div class="row">

                                                <asp:Label ID="Label19" runat="server" CssClass=" lblTxt lblName lblName2">Lenght of Service</asp:Label>
                                                <asp:Label ID="lblAbsent" runat="server" CssClass=" smLbl_to lblStyle"> lofserv</asp:Label>

                                            </div>

                                            <div class="row">

                                                <asp:Label ID="Label21" runat="server" CssClass=" lblTxt lblName lblName2">Last Leave Enjoyed Date</asp:Label>
                                                <asp:Label ID="lblHoliday" runat="server" CssClass=" smLbl_to lblStyle"> lastledate</asp:Label>

                                            </div>


                                        </div>

                                    </div>
                                </div>



                            </div>
                        </fieldset>
                    </div>
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="ViewEmpLeave" runat="server">

                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblleavesummary" runat="server" CssClass="btn btn-success btn-sm headPart" Visible="False">LEAVE SUMMARY</asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                <asp:GridView ID="gvLeaveStatus" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" Width="567px" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    OnRowDataBound="gvLeaveStatus_RowDataBound">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Desription">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDescription" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Opening Bal.">
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFOpening" runat="server"
                                                    Style="color: white; font-size: 11px; font-weight: bold;" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvopnleave" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opleave")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Entitled Leave">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvleaveentitled" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "enleave")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFleaveentitled" runat="server"
                                                    Style="color: white; font-size: 11px; font-weight: bold;" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Enjoyed">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvleaveenjoy" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "enjleave")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFleaveenjoy" runat="server"
                                                    Style="color: white; font-size: 11px; font-weight: bold;" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Balance">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvleavebal" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balleave")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFleavebal" runat="server"
                                                    Style="color: white; font-size: 11px; font-weight: bold;" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDescription0" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "descrip")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
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
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblleavesDetails" runat="server" CssClass="btn btn-success btn-sm headPart" Visible="False">LEAVE DETAILS</asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView ID="gvLeavedetails" runat="server" AutoGenerateColumns="False"
                                        ShowFooter="True" Width="567px" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        OnRowDataBound="gvLeavedetails_RowDataBound">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Desription">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvDescriptionld" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Apply Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvapplydate" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aplydat"))%>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Leave Applied(From)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvfrmdate" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "strtdat")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Leave Applied(End)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvenddate" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "enddat")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Leave Days">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvleavedays" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lvday")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFleavedays" runat="server"
                                                        Style="color: white; font-size: 11px; font-weight: bold;" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Reasons">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvleavereason" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lreason")) %>'
                                                        Width="200px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" />
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


                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
