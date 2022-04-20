<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptEmpMonthPresent.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_83_Att.RptEmpMonthPresent" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
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
                div#ContentPlaceHolder1_ddlCompany_chzn{
                       width:100%!important;
        }
                }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
            $('#<%=this.gvMonthlyPresence.ClientID %>').tblScrollable();
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
                        <div class="col-lg-3 col-md-3 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server">Company</asp:Label>
                                <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control chzn-select">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-1 col-md-2 col-sm-6">
                             <div class="form-group">
                                        <asp:Label ID="lblfrmdate" runat="server" >From</asp:Label>
                                        <asp:TextBox ID="txtFdate" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtFdate_CalendarExtender" runat="server"
                                           Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtFdate"></cc1:CalendarExtender>
                              </div>
                        </div>

                        <div class="col-lg-1 col-md-2 col-sm-6">
                             <div class="form-group">
                                 <asp:Label ID="Label1" runat="server">To</asp:Label>
                                        <asp:TextBox ID="txtTdate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtTdate"></cc1:CalendarExtender>
                                      
                              </div>
                          </div>
                        <div class="col-lg-1 col-md-1 col-sm-6">
                              <asp:LinkButton ID="lnkbtnShow" OnClick="lnkbtnShow_OnClick" runat="server" CssClass="btn btn-primary btn-sm mt20">Ok</asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div class="card-body">
                     <asp:GridView ID="gvMonthlyPresence" runat="server" AutoGenerateColumns="False"
                                ShowFooter="false" CssClass="table-striped table-hover table-bordered grvContentarea">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Section">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsection" runat="server" Text='<%#Convert.ToString( DataBinder.Eval(Container.DataItem, "section")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Employee Name">
                                        <ItemTemplate>
                                             <asp:Label ID="lblname" runat="server" Text='<%#Convert.ToString( DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldesig" runat="server" Text='<%#Convert.ToString( DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Working</br> Day">
                                        <ItemTemplate>
                                             <asp:Label ID="lblwkd" runat="server"
                                            Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "wd")) %>'
                                            Width="40px"></asp:Label>
                                        </ItemTemplate>
                                       
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Present">
                                        <ItemTemplate>
                                             <asp:Label ID="lblpre" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "present")).ToString("#,##0;(#,##0); ") %>'
                                            Width="40px"></asp:Label>
                                        </ItemTemplate>
                                       
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Absent">
                                        <ItemTemplate>
                                             <asp:Label ID="lblabs" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "absnt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Holiday">
                                        <ItemTemplate>
                                             <asp:Label ID="lblhold" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "holiday")).ToString("#,##0;(#,##0); ") %>'
                                            Width="40px"></asp:Label>
                                        </ItemTemplate>
                                       
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Leave">
                                        <ItemTemplate>
                                             <asp:Label ID="lblleave" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "noleav")).ToString("#,##0;(#,##0); ") %>'
                                            Width="40px"></asp:Label>
                                        </ItemTemplate>
                                       
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                    </asp:TemplateField>

                                   <asp:TemplateField HeaderText="Late">
                                        <ItemTemplate>
                                             <asp:Label ID="lbllate" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "late")).ToString("#,##0;(#,##0); ") %>'
                                            Width="40px"></asp:Label>
                                        </ItemTemplate>
                                       
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />

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


      
  
                       
     
        
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

