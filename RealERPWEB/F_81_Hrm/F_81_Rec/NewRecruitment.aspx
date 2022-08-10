<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="NewRecruitment.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_81_Rec.NewRecruitment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .mt20 {
            margin-top: 20px;
        }

        .chzn-drop {
            width: 100% !important;
        }

        .chzn-container {
            width: 100% !important;
        }

        .chzn-container-single .chzn-single {
            height: 35px !important;
            line-height: 35px !important;
        }

        .card-body {
            min-height: 400px !important;
        }

        .pd4 {
            padding: 4px !important;
        }
    </style>

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

            $('.chzn-select').chosen({ search_contains: true });
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
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-5">

                       <%-- <div class="form-group row">
                            <asp:Label ID="lblname" runat="server" CssClass="col-4">Name</asp:Label>
                            <asp:TextBox ID="txtname" runat="server" CssClass="form-control form-control-sm pd4 col-8"></asp:TextBox>
                        </div>

                        <div class="form-group row">
                            <asp:Label ID="lbldept" runat="server" CssClass="col-4">Department</asp:Label>
                             <asp:DropDownList ID="ddldept" runat="server"  CssClass="form-control form-control-sm pd4 col-8"  AutoPostBack="True" ></asp:DropDownList>
                        </div>

                                    <div class="form-group row">
                            <asp:Label ID="lbldesig" runat="server" CssClass="col-4">Designation</asp:Label>
                             <asp:DropDownList ID="ddldesig" runat="server"  CssClass="form-control form-control-sm pd4 col-8"  AutoPostBack="True" ></asp:DropDownList>
                        </div>
                        <div class="form-group row">
                            <asp:Label ID="lblmobile" runat="server" CssClass="col-4">Mobile No</asp:Label>
                            <asp:TextBox ID="txtmobile" runat="server" CssClass="form-control form-control-sm pd4 col-8"></asp:TextBox>
                        </div>
                        <div class="form-group row">
                            <asp:Label ID="lblemail" runat="server" CssClass="col-4">Email Address</asp:Label>
                            <asp:TextBox ID="txtemail" runat="server" CssClass="form-control form-control-sm pd4 col-8"></asp:TextBox>
                        </div>
                        <div class="form-group row">
                            <asp:Label ID="lblpreAdd" runat="server" CssClass="col-4">Present Address</asp:Label>
                            <asp:TextBox ID="txtPreAdd" runat="server" CssClass="form-control form-control-sm pd4 col-8"></asp:TextBox>
                        </div>
                        <div class="form-group row">
                            <asp:Label ID="lblPerAdd" runat="server" CssClass="col-4">Permanent Address</asp:Label>
                            <asp:TextBox ID="txtPerAdd" runat="server" CssClass="form-control form-control-sm pd4 col-8"></asp:TextBox>
                        </div>

                        <div class="form-group row">
                            <asp:Label ID="lblfile" runat="server" Text="Attach File:" CssClass="col-4"></asp:Label>
                            <asp:FileUpload ID="imgFileUpload" CssClass="form-control col-8" runat="server" AllowMultiple="true" accept=".pdf" />
                            <asp:RequiredFieldValidator ForeColor="Red" runat="server" ControlToValidate="imgFileUpload" ValidationGroup="group1" ErrorMessage="Please enter an image" />
                        </div>
                                   <div class="form-group row">
                        <asp:LinkButton ID="lnkSave" ValidationGroup="group1" CssClass="btn btn-success btn-sm mt20" runat="server" OnClick="lnkSave_Click">Save</asp:LinkButton>
           
                        </div>--%>


                                    <asp:GridView ID="gvNewRec" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" CssClass="table-condensed tblborder grvContentarea ml-3 visibleshow">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Code" ControlStyle-CssClass="classhidden" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgcode" ClientIDMode="Static" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                  
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lgcResDesc1" runat="server" Width="130px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvgph" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph")) %>'
                                            Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle Font-Bold="True" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvVal" ClientIDMode="Static" runat="server" BackColor="Transparent" CssClass="ml-1 form-control"
                                            BorderColor="#660033" BorderStyle="None" BorderWidth="1px" AutoPostBack="true"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "value")) %>'></asp:TextBox>

                                        <asp:TextBox ID="txtgvdVal" runat="server" BackColor="Transparent"
                                            BorderColor="#660033" BorderStyle="None" BorderWidth="1px" TextMode="Time"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "value")) %>'></asp:TextBox>


                                        <asp:Panel ID="Panegrd" runat="server">
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlval" runat="server" Width="300px" CssClass="custom-select chzn-select">
                                                </asp:DropDownList>
                                            </div>
                                        </asp:Panel>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>



                            </div>
                        <div class="col-7">


                         </div>
                    </div>

                </div>
            </div>




        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkSave" />
        </Triggers>

    </asp:UpdatePanel>
</asp:Content>

