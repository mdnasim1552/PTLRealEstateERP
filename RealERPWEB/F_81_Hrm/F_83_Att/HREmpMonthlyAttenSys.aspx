<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="HREmpMonthlyAttenSys.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_83_Att.HREmpMonthlyAttenSys" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        div#ContentPlaceHolder1_ddlEmpName_chzn {
            width: 100% !important;
        }

        .mt20 {
            margin-top: 20px;
        }

        .chzn-drop {
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
                    <div class="row">
                          <div class="col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="Label16" runat="server" Text="Month"></asp:Label>

                                <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged"
                                    CssClass="form-control form-control-sm" TabIndex="3"
                                    >
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="Label13" runat="server" CssClass="lblTxt lblName" Text="Emp.  Name">
                                    <asp:LinkButton ID="imgbtnEmployee" runat="server" OnClick="imgbtnEmployee_Click" TabIndex="2"><i class="fa fa-search"> </i></asp:LinkButton>
                                </asp:Label>
                                <asp:DropDownList ID="ddlEmpName" AutoPostBack="True" runat="server" CssClass="form-control chzn-select" TabIndex="3">
                                </asp:DropDownList>
                            </div>
                        </div>



                      
                        <div class="col-lg-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary btn-sm mt20" OnClick="lnkbtnShow_Click">Show</asp:LinkButton>

                            </div>
                        </div>
                        <div class="col-lg-1 mt20">
                             <asp:CheckBox ID="chckAll" runat="server" Text="Update All" Visible="false" />  
                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <asp:GridView ID="gvMonthlyAttn" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        AllowPaging="false" OnPageIndexChanging="gvMonthlyAttn_PageIndexChanging" Width="733px"
                        OnRowDeleting="gvMonthlyAttn_RowDeleting">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="SL">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvDate" runat="server" Height="16px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cdate")).ToString("dd-MMM-yyyy") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lFinalUpdate" runat="server" CssClass="btn btn-success btn-xs" OnClick="lFinalUpdate_Click">Final Update</asp:LinkButton>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Off. Intime">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvoffIntime" runat="server" Height="16px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "offintime")).ToString("hh:mm tt") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                             <%--   <FooterTemplate>
                                    <asp:LinkButton ID="lFinalTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="#000" OnClick="lFinalTotal_Click" CssClass="btn btn-primary primarygrdBtn">Total</asp:LinkButton>
                                </FooterTemplate>--%>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Off. Outtime">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvoffouttime" runat="server" Height="16px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "offouttime")).ToString("hh:mm tt") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ac. Intime">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvIntime" runat="server" BackColor="Transparent" BorderStyle="None"
                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "intime")).ToString("hh:mm tt") %>'
                                        Width="60px" Font-Size="11px" ></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ac. Outtime">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvOuttime" runat="server" BackColor="Transparent" BorderStyle="None"
                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "outtime")).ToString("hh:mm tt") %>'
                                        Width="60px" Font-Size="11px" ></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ln Intime" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvlnintime" runat="server" Height="16px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lnchintime")).ToString("hh:mm tt") %>'
                                        Width="60px" Font-Size="11px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ln Outtime" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvlnouttime" runat="server" Height="16px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lnchouttime")).ToString("hh:mm tt") %>'
                                        Width="60px" Font-Size="11px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
               <%--             <asp:TemplateField HeaderText="Leave">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvLeave" runat="server" BackColor="Transparent" BorderStyle="None"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "leav")) %>'
                                        Width="30px" Font-Size="11px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Absent">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvAbsent" runat="server" BackColor="Transparent" BorderStyle="None"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "absnt")) %>'
                                        Width="40px" Font-Size="11px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Holiday">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvholiday" runat="server" BackColor="Transparent" BorderStyle="None"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hday")) %>'
                                        Width="40px" Font-Size="11px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>--%>

                        </Columns>
                        <FooterStyle CssClass="grvFooter" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                    </asp:GridView>

                </div>
            </div>



        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

