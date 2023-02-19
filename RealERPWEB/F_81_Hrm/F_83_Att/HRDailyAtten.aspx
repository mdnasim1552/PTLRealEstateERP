<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="HRDailyAtten.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_83_Att.HRDailyAtten" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <style>
        .mt20 {
            margin-top: 20px;
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
    <script type="text/javascript">

        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            try {

                var gv = $('#<%=this.gvDailyAttn.ClientID%>');
                gv.Scrollable();
            }
            catch (e) {

                alert(e);
            }
        };


    </script>

</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



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
                        <div class="col-lg-1 col-md-3 col-sm-3">
                            <div class="form-group">

                                <asp:Label ID="Label10" runat="server">Date</asp:Label>
                                <asp:TextBox ID="txtdate" runat="server" CssClass=" form-control form-control-sm pd4 w100"></asp:TextBox>
                                <cc1:CalendarExtender ID="csefdate" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-lg-1 col-md-3 col-sm-3">
                            <div class="form-group">
                                <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName">Page Size</asp:Label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="w100 form-control form-control-sm" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem>300</asp:ListItem>
                                    <asp:ListItem>500</asp:ListItem>
                                    <asp:ListItem>1000</asp:ListItem>
                                    <asp:ListItem>1500</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-4">
                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-danger btn-sm mt20 " OnClick="lbtnOk_Click">Upload Data <i class="fa fa-plus"></i></asp:LinkButton>
                            <asp:LinkButton ID="lbtnShow" runat="server" CssClass="btn btn-primary btn-sm mt20" OnClick="lbtnShow_Click">Show</asp:LinkButton>
                            <asp:CheckBox ID="chktype" runat="server" TabIndex="6" Text="Level-19" CssClass="btn btn-primary checkBox" />

                        </div>

                    </div>
                </div>
                <div class="card-body">
                    <div class="row">
                        <asp:GridView ID="gvDailyAttn" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                            AllowPaging="True" OnPageIndexChanging="gvDailyAttn_PageIndexChanging">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Company & Section Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvsection" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "comname")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "section").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "comname")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "section")).Trim(): "")  %>'
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Emp. ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvEmpId" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Card #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvEmpIDCard" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee Name & Designation ">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lFinalUpdate" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="White" OnClick="lFinalUpdate_Click" CssClass="btn btn-danger  primarygrdBtn ">Final Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvEmpName" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "desig").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")).Trim(): "") %>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Off. Intime">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvoffIntime" runat="server" Height="16px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "offintime")).ToString("hh:mm tt") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
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
                                            Width="60px" Font-Size="11px"></asp:TextBox>

                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ac. Outtime">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvOuttime" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "outtime")).ToString("hh:mm tt") %>'
                                            Visible='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "intime")).ToString("hh:mm tt") == Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "outtime")).ToString("hh:mm tt") ?false : true) %>'
                                            Width="60px" Font-Size="11px"></asp:TextBox>
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
