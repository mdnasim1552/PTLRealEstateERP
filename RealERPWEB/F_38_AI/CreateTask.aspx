<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="CreateTask.aspx.cs" Inherits="RealERPWEB.F_38_AI.CreateTask" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-12" style="background:#F6F7F9">
                            <div class="form-group row">
                                <div class="p-0 col-lg-6 col-md-6 col-sm-12">
                                    <asp:Label ID="Label14" runat="server">Date</asp:Label>
                                    <asp:TextBox ID="Txtdate" runat="server" CssClass="form-control"></asp:TextBox>
                                    <cc1:CalendarExtender runat="server" Format="dd-MMM-yyyy" TargetControlID="Txtdate"></cc1:CalendarExtender>
                                </div>

                                <div class=" col-lg-6 col-md-6 col-sm-12">
                                    <asp:Label ID="Label3" runat="server">Order Type</asp:Label>
                                    <asp:DropDownList ID="ddlordertype" runat="server" CssClass="form-control chzn-select">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group row">
                                <asp:Label ID="Label1" runat="server">Customer</asp:Label>
                                <asp:DropDownList ID="ddlcustomer" runat="server" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlcustomer_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                            <div class="form-group row">
                                <asp:Label ID="Label2" runat="server">Project</asp:Label>
                                <asp:DropDownList ID="ddlproject" runat="server" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlproject_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>

                            <div class="form-group row">
                                <asp:Label ID="Label5" runat="server">Project Type</asp:Label>
                                <asp:DropDownList ID="ddlprotype" runat="server" CssClass="form-control chzn-select" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>

                            <div class="form-group row">
                                <div class="col-lg-6 col-md-6 col-sm-12 pl-0">
                                    <asp:Label ID="Label6" runat="server">Work Type</asp:Label>
                                    <asp:DropDownList ID="ddlworktype" runat="server" CssClass="form-control chzn-select" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-12">
                                    <asp:Label ID="Label7" runat="server">DataSet</asp:Label>
                                    <asp:DropDownList ID="ddldataset" runat="server" CssClass="form-control chzn-select" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                            </div>




                        </div>
                        <div class="col-lg-4  col-md-4 col-sm-12">
                            <div class="form-group row">
                                <div class="col-lg-6 col-md-6 col-sm-12 pl-0 ">
                                    <asp:Label ID="Label4" runat="server">Batch</asp:Label>
                                    <asp:DropDownList ID="ddlbatch" runat="server" CssClass="form-control chzn-select">
                                        <asp:ListItem Value="1">1</asp:ListItem>
                                        <asp:ListItem Value="2">2</asp:ListItem>
                                        <asp:ListItem Value="3">3</asp:ListItem>
                                        <asp:ListItem Value="4">4</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-12">
                                    <asp:Label ID="Label15" runat="server">Dataset Quantity</asp:Label>
                                    <asp:TextBox ID="txtworkquantity" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <asp:Label ID="Label11" runat="server">Task Title</asp:Label>
                                <asp:TextBox ID="txttasktitle" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            </div>


                            <div class="form-group row">
                                <asp:Label ID="Label12" runat="server">Task Description</asp:Label>
                                <asp:TextBox ID="txtdesc" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="7"></asp:TextBox>
                            </div>
                            <div class="form-group row d-none">
                                <asp:Label ID="Label13" runat="server">Remakrs</asp:Label>
                                <asp:TextBox ID="txtremaks" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            </div>

                        </div>
                        <div class=" col-lg-5  col-md-5 col-sm-12">

                            <div class="form-group row">
                                
                                    <asp:Label ID="Label16" runat="server">Assign Team Members</asp:Label>
                                    <asp:DropDownList ID="ddlassignmember" runat="server" CssClass="form-control chzn-select" AutoPostBack="true">
                                    </asp:DropDownList>
                                
                                
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-6 col-md-6 col-sm-12 pl-0">
                                    <asp:Label ID="Label8" runat="server">Valocity Type</asp:Label>
                                    <asp:DropDownList ID="ddltasktype" runat="server" CssClass="form-control chzn-select" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-12">
                                    <asp:Label ID="Label9" runat="server"> Valocity Quantity</asp:Label>
                                    <asp:TextBox ID="txtquantity" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                            </div>
                            <div class="form-group row">
                                <div class=" col-lg-3 col-md-3 col-sm-12 pl-0">
                                    <asp:Label ID="Label10" runat="server">Work Hour</asp:Label>
                                    <asp:TextBox ID="txtworkhour" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-12">
                                    <asp:Label ID="Label17" runat="server">Annotation ID</asp:Label>
                                    <asp:DropDownList ID="ddlAnnotationid" runat="server" CssClass="form-control chzn-select" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <div class=" col-lg-3 col-md-3 col-sm-12 mt-4 ">
                                    <asp:LinkButton ID="btnaddrow" runat="server" OnClick="btnaddrow_Click" CssClass=" btn btn-primary ml-auto btn-sm mt20 mr-1 float-left"><i class="fa fa-plus"></i></asp:LinkButton>

                                </div>
                            </div>

                            <div class="form-group">
                                 <asp:GridView ID="GridVirtual" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" Width="">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="SL # ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right; font-size: 12px;"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"
                                            ForeColor="Black"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Member">
                                    <ItemTemplate>
                                        <asp:Label ID="lblmember" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lblmember")) %>' Width="120px" ForeColor="Black" Font-Size="12px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Annotation ID">
                                    <ItemTemplate>
                                         <asp:Label ID="tblAnnotation" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "anotation")) %>'  ForeColor="Black" Font-Size="12px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Valocity  <br> Type">
                                    <ItemTemplate>
                                        <asp:Label ID="tbltype" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tbltype")) %>'  ForeColor="Black" Font-Size="12px"></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Valocity <br> QTY">
                                    <ItemTemplate>
                                        <asp:Label ID="tblValoquantity" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tblValoquantity")) %>'   ForeColor="Black" Font-Size="12px"></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Work <br> Hour">
                                    <ItemTemplate>
                                        <asp:Label ID="tblworkhour" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tblworkhour")) %>'  ForeColor="Black" Font-Size="12px"></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <%--<FooterStyle CssClass="grvFooter" />--%>
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                            </div>


                        </div>
                    </div>
                    <hr />
                 
                    <hr />
                    <div class="table-responsive">
                       
                    </div>
                    <hr />
                    <div class="row ">
                        <div class="col-lg-12 col-md-12 col-sm-22 mt-4">
                            <div class="form-group text-center">
                                <asp:LinkButton ID="btntaskcreate" runat="server" OnClick="btntaskcreate_Click" CssClass=" btn btn-primary btn-sm mt20"><i class="fas fa-plus">&nbsp;CreateTask</i></asp:LinkButton></li>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
