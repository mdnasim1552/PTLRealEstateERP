<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="HREmpTransferReport.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_87_Tra.HREmpTransferReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {
            try {

                $(".chosen-select").chosen({
                    search_contains: true,
                    no_results_text: "Sorry, no match!",
                    allow_single_deselect: true
                });
                $('.chosen-continer').css({ 'width': '600px', "height": "20px" });

                $('.chzn-select').chosen({ search_contains: true });

            }

            catch (e) {

                alert(e.message);
            }

        }

    </script>

    <style>
        .switch {
            position: relative;
            display: inline-block;
            width: 40px;
            height: 20px;
        }

            .switch input {
                opacity: 0;
                width: 0;
                height: 0;
            }

        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 18px;
                width: 18px;
                left: 1px;
                bottom: 1px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
            }

        input:checked + .slider {
            background-color: #2196F3;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #2196F3;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(18px);
            -ms-transform: translateX(18px);
            transform: translateX(18px);
        }

        /* Rounded sliders */
        .slider.round {
            border-radius: 20px;
        }

            .slider.round:before {
                border-radius: 50%;
            }



        .container-data {
            box-sizing: border-box;
        }
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

            <div class="card" style="min-height: 600px">
                <div class="card-body">
                    <div class="form-group">
                        <div class="row">

                            <div class="col-md-2 col-sm-3" style="margin: 2px;">
                                <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt">From</asp:Label>
                                <asp:TextBox ID="txtFromDate" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtFromDate"></cc1:CalendarExtender>
                            </div>
                            <div class="col-md-2 col-sm-3" style="margin: 2px;">
                                <asp:Label ID="lbltodate" runat="server" CssClass="lblTxt">To</asp:Label>
                                <asp:TextBox ID="txtToDate" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtToDate_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtToDate"></cc1:CalendarExtender>
                            </div>

                            <div class="col-md-1" style="margin: 2px;">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                            </div>


                            <div class="col-md-2 pading5px " style="margin: 2px;">
                                <asp:Label ID="lblpagem" runat="server" CssClass=" smLbl_to" Text="Page Size"></asp:Label>

                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" TabIndex="3">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem>300</asp:ListItem>
                                    <asp:ListItem>600</asp:ListItem>
                                    <asp:ListItem>900</asp:ListItem>
                                    <asp:ListItem>1200</asp:ListItem>
                                </asp:DropDownList>

                            </div>

                        </div>
                    </div>
                    <asp:GridView ID="gvEmpInfo" runat="server" AutoGenerateColumns="False" CssClass="table-condensed table-hover table-bordered grvContentarea"
                        PagerSettings-Position="Bottom" ShowFooter="True" OnPageIndexChanging="gvEmpInfo_PageIndexChanging" PageSize="10" AllowPaging="true">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="ID Card ">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvidcard" runat="server" Style="text-align: center"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                        Width="50px" Font-Size="12PX"></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvempname" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                        Width="200px" Font-Size="12PX"></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="From Company">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvfcompany" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fcompany")) %>'
                                        Width="200px" Font-Size="12PX"></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="To Company">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvtcompany" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tcompany")) %>'
                                        Width="200px" Font-Size="12PX"></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="From Department">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvfdepart" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fdepart")) %>'
                                        Width="120px" Font-Size="12PX"></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="To Department">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvtdepart" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tdepart")) %>'
                                        Width="120px" Font-Size="12PX"></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Designation">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvhrgdesc" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgdesc")) %>'
                                        Width="140px" Font-Size="12PX"></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvtdate" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "tdate")).ToString("dd-MMM-yyyy") %>'
                                        Width="100px" Font-Size="12PX"></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>



                        </Columns>
                        <PagerStyle CssClass="gvPagination" />
                        <PagerSettings Mode="NumericFirstLast" />
                        <HeaderStyle CssClass="grvHeader" />
                        <FooterStyle CssClass="grvFooter" />
                        <RowStyle CssClass="grvRows" />
                        <AlternatingRowStyle />

                    </asp:GridView>


                </div>


            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

