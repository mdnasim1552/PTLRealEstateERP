<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="Occasion.aspx.cs" Inherits="RealERPWEB.Notification.Occasion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* The switch - the box around the slider */
        .switch {
            position: relative;
            display: inline-block;
            width: 55px;
            height: 30px;
            float: right;
        }

            /* Hide default HTML checkbox */
            .switch input {
                display: none;
            }

        /* The slider */
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
                height: 26px;
                width: 26px;
                left: 4px;
                bottom: 4px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
            }

        input.default:checked + .slider {
            background-color: #444;
        }

        input.primary:checked + .slider {
            background-color: #2196F3;
        }

        input.success:checked + .slider {
            background-color: #8bc34a;
        }

        input.info:checked + .slider {
            background-color: #3de0f5;
        }

        input.warning:checked + .slider {
            background-color: #FFC107;
        }

        input.danger:checked + .slider {
            background-color: #f44336;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #2196F3;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(26px);
            -ms-transform: translateX(26px);
            transform: translateX(26px);
        }

        /* Rounded sliders */
        .slider.round {
            border-radius: 30px;
        }

            .slider.round:before {
                border-radius: 50%;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
            <div class="card card-fluid container-data mt-5" id='printarea'>
                <div class="card-body">
                    <div class="card-header">
                        <div class="row">
                            <div class="col-md-12">
                                <h4>Occasion Details</h4>
                            </div>
                        </div>
                    </div>

                    <div class="row d-none">
                        <div class="col-md-4 col-sm-6 col-lg-4">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary" type="button">From</button>
                                </div>
                                <asp:TextBox ID="txtFdate" runat="server" autocomplete="off" CssClass="from-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtFdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtFdate"></cc1:CalendarExtender>
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary" type="button">To</button>
                                </div>
                                <asp:TextBox ID="txtTdate" runat="server" autocomplete="off" CssClass="from-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtTdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtTdate"></cc1:CalendarExtender>

                            </div>
                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="xcard-box">
                                <ul class="nav nav-pills navtab-bg nav-justified">
                                    <li class="nav-item">
                                        <a href="#birthday" data-toggle="tab" aria-expanded="false" class="nav-link active">Birthday</a>
                                    </li>

                                    <li class="nav-item" id="tbAssignTask" runat="server" visible="false">
                                        <a href="#marriageday" data-toggle="tab" aria-expanded="false" class="nav-link">Marriage Day</a>
                                    </li>

                                </ul>
                                <div class="tab-content" id="myTabContent">
                                    <div class="tab-pane active" id="birthday">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvOccasion" runat="server" AutoGenerateColumns="false"
                                                CssClass="table table-bordered table-striped display" AllowPaging="True" ViewStateMode="Enabled"
                                                AllowSorting="True" PageSize="500">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle />
                                                        <ItemStyle />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProsCode" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"proscode")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle />
                                                        <ItemStyle />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Prospect Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProspectName" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prosname"))%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle />
                                                        <ItemStyle />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Email">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProsEmail" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"prosemail")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle />
                                                        <ItemStyle />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Phone">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProsPhone" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"prosphone")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle />
                                                        <ItemStyle />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Address">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProsAdd" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"prospreadd")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle />
                                                        <ItemStyle />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Occasion Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOccasionName" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"occasionname")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle />
                                                        <ItemStyle />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Date & Time">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDateTime" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "occdatetime")).ToString("dddd, dd MMM yyy, HH:mm tt") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="SMS">
                                                        <ItemTemplate>
                                                            <label class="switch " title="Send SMS">
                                                                <input type="checkbox" id="chkbSMS" runat="server" class="info">
                                                                <span class="slider round"></span>
                                                            </label>
                                                        </ItemTemplate>
                                                        <HeaderStyle />
                                                        <ItemStyle Width="20px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Email">
                                                        <ItemTemplate>
                                                            <label class="switch " title="Send Email">
                                                                <input type="checkbox" id="chkbEmail" runat="server" class="primary">
                                                                <span class="slider round"></span>
                                                            </label>
                                                        </ItemTemplate>
                                                        <HeaderStyle />
                                                        <ItemStyle Width="20px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Action" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnMesg" Text="Message" runat="server" CssClass="btn-text "><span class="glyphicon glyphicon-envelope" style="margin-top:5px;" ></span> Message</asp:LinkButton>
                                                            <asp:LinkButton ID="lnkView" Text="View" runat="server" OnClientClick="NewWindow();" CssClass="btn-text"><span class="glyphicon glyphicon-eye-open" style="margin-top:5px; margin-left:10px"></span> View</asp:LinkButton>
                                                            <asp:LinkButton ID="lnkdele" Text="Delete" runat="server" CssClass="btn-text " Visible="false"><span class="glyphicon glyphicon-trash" style="margin-top:5px; margin-left:10px""></span> Delete</asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle />
                                                        <ItemStyle />
                                                    </asp:TemplateField>

                                                </Columns>
                                                <PagerStyle CssClass="gvPagination" />
                                                <EmptyDataTemplate>
                                                    <div style="color: red; text-align: center !important; font-style: italic; font-size: 15px;">No records to display.</div>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <script>

                $(document).ready(function () {
                    $('#ContentPlaceHolder1_gvNotificaitons').DataTable();
                });

            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
