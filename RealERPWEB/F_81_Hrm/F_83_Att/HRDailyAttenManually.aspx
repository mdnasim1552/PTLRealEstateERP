<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="HRDailyAttenManually.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_83_Att.HRDailyAttenManually" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        div#ContentPlaceHolder1_ddlCompany_chzn {
            width: 100% !important;
}
        div#ContentPlaceHolder1_ddlProjectName_chzn {
                        width: 100% !important;
}
        

div#ContentPlaceHolder1_ddlSection_chzn {
                     width: 100% !important;
}

        .mt20 {
            margin-top: 20px;
        }

        .w100 {
            width: 100% !important;
        }
        .chzn-drop {
            width:100%!important;
}
                .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
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

               

                    <div class="card card-fluid mt-5">
                        <div class="card-header">
                            <div class="row">
                                <div class="col-lg-1 col-md-1 col-sm-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label10" runat="server">Date</asp:Label>
                                        <asp:TextBox ID="txtdate" runat="server" CssClass=" form-control form-control-sm "></asp:TextBox>
                                        <cc1:CalendarExtender ID="csefdate" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label1" runat="server">Company</asp:Label>
                                        <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label2" runat="server">Department</asp:Label>
                                        <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control  chzn-select" TabIndex="7" AutoPostBack="True" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-3  col-md-3 col-sm-4">
                                    <div class="form-group">
                                        <asp:Label ID="lblSection" runat="server">Section</asp:Label>
                                        <asp:DropDownList ID="ddlSection" runat="server" CssClass="form-control chzn-select" TabIndex="7">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-2  col-md-2 col-sm-4">
                                                     <asp:Label ID="Label3" runat="server">Code</asp:Label>
                                    <div class="input-group input-group-sm mb-3">
                                          <asp:TextBox ID="txtSrcEmployee" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        <div class="input-group-append">
                                            <asp:LinkButton ID="imgbtnSearchEmployee" runat="server" CssClass="btn btn-secondary btn-sm" OnClick="imgbtnSearchEmployee_Click"><span class="fa fa-search"> </span></asp:LinkButton>
                                        </div>
                                    </div>
                      
                                </div>
                               
                                 
                                <div class="col-lg-1 col-md-1 col-sm-6">
                                    <div class="form-group ">
                                        <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName ">Page Size</asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
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
                                 <div class="col-lg-2 col-md-2 col-sm-4">
                                    <a class="btn btn-info btn-sm mt20" href="<%=this.ResolveUrl("~/F_81_Hrm/F_83_Att/HRDailyAttenUpload.aspx")%>">Auto</a>
                               
                                    <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm mt20 " OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                </div>
                            </div>





                        </div>
                        <div class="card-body">
                            <div class="row table-responsive">
                                <asp:GridView ID="gvDailyAttn" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                    AllowPaging="True" OnPageIndexChanging="gvDailyAttn_PageIndexChanging"
                                    CssClass="table-striped table-hover table-bordered grvContentarea"
                                    OnRowDeleting="gvDailyAttn_RowDeleting">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="True" />
                                        <asp:TemplateField HeaderText="Section Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvsection" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Emp. ID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvEmpId" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                    Width="130px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Card #">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvEmpIDCard" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Employee Name ">
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lFinalUpdate" runat="server" CssClass="btn btn-success btn-sm" OnClick="lFinalUpdate_Click">Final Update</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvEmpName" runat="server" Height="16px" Text='<%# "<b>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))+"</b>" %>'
                                                    Width="250px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText=" Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDesig" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Off. Intime">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvoffIntime" runat="server" Height="16px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "offintime")).ToString("hh:mm tt") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Off. Outtime">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvoffouttime" runat="server" Height="16px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "offouttime")).ToString("hh:mm tt") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ac. Intime">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvIntime" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "intime")).ToString("hh:mm tt") %>'
                                                    Width="60px" Font-Size="11px" Visible='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "leave")).Trim())!="L" &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "absnt")).Trim())!="A" %>'></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ac. Outtime">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvOuttime" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "outtime")).ToString("hh:mm tt") %>'
                                                    Width="55px" Font-Size="11px" Visible='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "leave")).Trim())!="L" &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "absnt")).Trim())!="A" %>'></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ln Intime" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlnintime" runat="server" Height="16px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lnchintime")).ToString("hh:mm tt") %>'
                                                    Width="55px" Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ln Outtime" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlnouttime" runat="server" Height="16px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lnchouttime")).ToString("hh:mm tt") %>'
                                                    Width="55px" Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Leave">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvLeave" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "leave")) %>'
                                                    Width="30px" Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Absent">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvAbsent" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "absnt")) %>'
                                                    Width="35px" Font-Size="11px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Deduction">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvDed" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dedout")).ToString("#,##0.00;(#,##.00); ") %>'
                                                    Width="35px" Font-Size="11px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Add. Hour">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvAddHour" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "addhour")).ToString("#,##0.00;(#,##.00); ") %>'
                                                    Width="40px" Font-Size="11px"></asp:TextBox>
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
                                <cc1:ListSearchExtender ID="ListSearchExtender1" runat="server" QueryPattern="Contains"
                                    TargetControlID="ddlProjectName">
                                </cc1:ListSearchExtender>
                            </div>
                        </div>
                    </div>

                   
           

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
