<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptServiceStoryProjectWise.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_97_MIS.RptServiceStoryProjectWise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            pageLoaded();

        });

        function pageLoaded() {


            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
            

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

            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-3 col-sm-3 col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="Label4" runat="server" Text="Project Name" CssClass="control-label"></asp:Label>
                                <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="custom-select  chzn-select" ></asp:DropDownList>
                            </div>
                        </div>
                         <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblPage" runat="server" CssClass="control-label" Text="Page Size"></asp:Label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"
                                    OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                   <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem>300</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>                       
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <asp:LinkButton ID="lbtnOk" runat="server"  CssClass="btn btn-primary btn-sm primaryBtn" Style="margin-top: 14px" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body" style="min-height: 400px;">
                    <div class="row table-responsive">
                        <asp:GridView ID="gvProjEmp" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" OnPageIndexChanging="gvProjEmp_PageIndexChanging"  AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>

                                <asp:TemplateField HeaderText="SL#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="ID#">
                                    <ItemTemplate>


                                        <asp:Label ID="lblgvIdcardno" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                            Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false" Target="_blank"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                            Width="80px">
                                        </asp:Label>

                                    </ItemTemplate>

                                   <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Employee Name">
                                    <ItemTemplate>


                                        <asp:Label ID="lblgvEmpname" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                            Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false" Target="_blank"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                            Width="150px">
                                        </asp:Label>

                                    </ItemTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>


                                        <asp:Label ID="lblgvDesig" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                            Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false" Target="_blank"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                            Width="150px">
                                        </asp:Label>

                                    </ItemTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Total Working Duration">
                                    <ItemTemplate>


                                        <asp:Label ID="lblgvSerlength" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                            Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false" Target="_blank"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "serlength")) %>'
                                            Width="150px">
                                        </asp:Label>

                                    </ItemTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                           

                            </Columns>

                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                            <RowStyle CssClass="grvRows" />
                        </asp:GridView>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
