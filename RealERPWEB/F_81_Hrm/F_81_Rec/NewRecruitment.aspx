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
            height: 28px !important;
            line-height: 28px !important;
        }

        .card-body {
            min-height: 400px !important;
        }

        .pd4 {
            padding: 4px !important;
        }

        table {
            border: 1px solid #CCC;
            border-collapse: collapse;
        }

        td, th {
            border: none;
        }

        .grvHeader {
            background: none !important;
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
                        <div class="col-4">



                            <asp:GridView ID="gvNewRec" runat="server" AutoGenerateColumns="False" BorderStyle="None" Width="100%"
                                CssClass="">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Code" ControlStyle-CssClass="classhidden" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgcode" ClientIDMode="Static" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgdesc" runat="server" Width="130px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgdesc")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <ItemTemplate>

                                            <asp:TextBox ID="txtgvVal" ClientIDMode="Static" runat="server" BackColor="Transparent" CssClass="ml-1 form-control form-control-sm"
                                                AutoPostBack="true"></asp:TextBox>

                                            <asp:TextBox ID="txtarea" ClientIDMode="Static" runat="server" BackColor="Transparent" CssClass="ml-1 form-control " TextMode="MultiLine"
                                                AutoPostBack="true"></asp:TextBox>


                                            <div class="form-group">
                                                <asp:DropDownList ID="ddldesig" runat="server" CssClass="custom-select chzn-select ">
                                                </asp:DropDownList>
                                            </div>



                                            <div class="form-group">

                                                <asp:FileUpload ID="imgFileUpload" CssClass="form-control form-control-sm" runat="server" accept=".pdf" />
                                                <%--                                      <asp:RequiredFieldValidator ForeColor="Red" runat="server" ControlToValidate="imgFileUpload" ValidationGroup="group1" ErrorMessage="Please enter an image" />--%>
                                            </div>
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
                            <asp:LinkButton ID="lnkSave" runat="server" CssClass="btn btn-sm btn-primary mx-2 my-2" OnClick="lnkSave_Click" Width="100px"
                                OnClientClick="return confirm('Are You Sure?')"><span class="fa fa-save " style="color:white;" aria-hidden="true"  ></span>&nbsp; Save</asp:LinkButton>


                        </div>
                        <div class="col-8">
                            <div class="card-body">
                                <div class="table table-sm table-responsive">
                                    <asp:GridView CssClass=" table-striped table-hover table-bordered" ID="gvAllRec" runat="server" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="ADVNO">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbladvno" runat="server" Text='<%#Eval("advno")%>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblname" runat="server" Text='<%#Eval("name").ToString()%>' Width="150px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldesig" runat="server" Text='<%#Eval("desig").ToString()%>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Mobile">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblmobile" runat="server" Text='<%#Eval("mobile").ToString()%>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Email">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblemail" runat="server" Text='<%#Eval("email").ToString()%>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                                                 <asp:TemplateField HeaderText="Permanent Address">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpreadd" runat="server" Text='<%#Eval("peradd").ToString()%>' Width="200px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                                                 <asp:TemplateField HeaderText="Present Address">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblperadd" runat="server" Text='<%#Eval("peradd").ToString()%>' Width="200px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>



<%--                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btn_edit" runat="server" CssClass="btn-sm text-info" OnClick="btn_edit_Click"> <i class="fa fa-edit"></i> 
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btn_remove" runat="server" CssClass="btn-sm text-danger" OnClick="btn_remove_Click"> <i class="fa fa-trash"></i> 
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
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

