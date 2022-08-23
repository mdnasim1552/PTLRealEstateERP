<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AnnualIncrement.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_93_AnnInc.AnnualIncrement" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;

        }
           .chzn-container{
             width: 100% !important;
        }

        .chzn-drop {
            width: 100% !important;
        }
                        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }
    </style>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
           <%-- var gvAnnIncre = $('#<%=this.gvAnnIncre.ClientID %>');
            gvAnnIncre.Scrollable();--%>

            <%--var gridview = $('#<%=this.gvAnnIncre.ClientID %>');
            $.keynavigation(gridview);--%>

            $('.chzn-select').chosen({ search_contains: true });

        };
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
                    <asp:Panel ID="pnlIncDetails" CssClass="mt-2" runat="server">
                        <div class="row">
                            <div class="col-md-3 col-sm-6 col-lg-2 col-xs-12">
                                <div class="form-group">
                                    <asp:Label ID="lblfrmdate" runat="server" class="control-label" Text="Increment Date"></asp:Label>
                                    <asp:TextBox ID="txtdate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Enabled="True"
                                        Format="dd.MM.yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-md-2 col-sm-6 col-lg-2 col-xs-12">
                                <div class="form-group">
                                    <asp:Label ID="lblIncNo0" runat="server" class="control-label" Text="Inc. No."></asp:Label>
                                    <asp:Label ID="lblCurIncrNo" runat="server" class="control-label" for="ReqNoCur"></asp:Label>
                                    <asp:TextBox ID="txtCurIncrNo" runat="server" CssClass="form-control form-control-sm" ReadOnly="true">00000</asp:TextBox>
                                </div>
                            </div>
                           
                            <div class="col-md-2 col-sm-6 col-lg-1 col-xs-12">
                                <div class="form-group">
                                    <asp:Label ID="Label1" runat="server" class="control-label" Text="Page"></asp:Label>
                                   <asp:DropDownList ID="ddlpagesize" runat="server" CssClass="form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                            <asp:ListItem Selected="True">600</asp:ListItem>
                                            <asp:ListItem>900</asp:ListItem>
                                        </asp:DropDownList>
                                </div>
                            </div>
                             <div class="col-md-1 col-sm-1 col-lg-1 col-xs-12">
                                <asp:LinkButton ID="lnkbtnShow" runat="server" Text="Ok" OnClick="lnkbtnShow_Click" CssClass="btn btn-primary btn-sm" Style="margin-top: 20px;"></asp:LinkButton>
                            </div>
                        <div class="col-lg-3"></div>

                            <div class="col-md-3 col-sm-6 col-lg-3 col-xs-12">
                                <asp:LinkButton ID="imgbtnPreList" runat="server" Text="Prev. Req.List" Font-Underline="false" OnClick="imgbtnPreList_Click"></asp:LinkButton>
                                <asp:DropDownList ID="ddlPrevIncList" runat="server" CssClass="form-control chzn-select form-control-sm"></asp:DropDownList>
                            </div>
                        </div>



                        <div class="row">
                            <div class="col-md-3 col-sm-6 col-lg-3 col-xs-12">
                                <div class="form-group">
                                    <asp:Label ID="lblCompany" runat="server" class="control-label" Text="Company"></asp:Label>
                                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control chzn-select form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-6 col-lg-3 col-xs-12">
                                <div class="form-group">
                                    <asp:Label ID="lblDepartment" runat="server" class="control-label" Text="Department"></asp:Label>
                                    <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control chzn-select form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-6 col-lg-3 col-xs-12">
                                <div class="form-group">
                                    <asp:Label ID="lblSection" runat="server" class="control-label" Text="Section"></asp:Label>
                                    <asp:DropDownList ID="ddlSection" runat="server" CssClass="form-control chzn-select form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                           
                       
                             <div class="col-md-3 col-sm-6 col-lg-2 col-xs-12">
                                <div class="form-group">
                                    <asp:Label ID="lblEmployee" runat="server" class="control-label">Employee</asp:Label>
                                    <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control chzn-select form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                             <div class="col-md-1 col-sm-1 col-lg-1 col-xs-12">
                                 <asp:LinkButton ID="linkAddEmp" runat="server"  CssClass="btn btn-success btn-sm " Style="margin-top: 20px;" OnClick="linkAddEmp_Click" ><i class="fas fa-plus "></i> Add</asp:LinkButton>
                                
                            </div>
                            
                            
                            
                            

                            
                        </div>
                    </asp:Panel>
                </div>
                <div class="card-body" style="min-height: 350px;">
                    <div class="table-responsive">
                        <asp:GridView ID="gvAnnIncre" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            OnPageIndexChanging="gvAnnIncre_PageIndexChanging" ShowFooter="True" CssClass="table-striped table-hover table-bordered">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>                                    
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText=" ">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnIncreEmp" runat="server" OnClick="btnIncreEmp_Click" CssClass="btn btn-xs" ToolTip="Delete Employee"> <i class="fas fa-trash" style="color:red;"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Emp ID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvEmpId" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                            Width="180px" Font-Bold="True" Font-Size="11px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />

                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Department">
                                    <ItemTemplate>
                                        <asp:Label ID="lgProName" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")) %>'
                                            Width="140px" Font-Bold="True"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>


                                        <asp:LinkButton ID="lbtnTotal" runat="server" OnClick="lbtnTotal_Click" CssClass="btn btn-primary btn-sm">Total</asp:LinkButton>



                                        <asp:LinkButton ID="lbtnRound" runat="server" OnClick="lbtnRound_Click" Style="float: right;" CssClass="btn btn-primary btn-sm">Round</asp:LinkButton>

                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Section">
                                    <HeaderTemplate>
                                        <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                            Text="Section" Width="90px"></asp:Label>

                                        <asp:HyperLink ID="hlbtntbCdataExel" runat="server" Font-Underline="false" CssClass="btn btn-success btn-xs" ToolTip="Export To Excel"><i class="fas fa-file-excel"></i></asp:HyperLink>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lgvSection" runat="server"
                                            Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnPutSameValue" runat="server" Font-Bold="True" Font-Underline="true"
                                            Font-Size="12px" OnClick="lbtnPutSameValue_Click" CssClass="btn btn-primary btn-sm">Put Same Value</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                </asp:TemplateField>



                                <%-- <asp:TemplateField HeaderText="Section">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnPutSameValue" runat="server" Font-Bold="True" Font-Underline="true"
                                            Font-Size="12px" ForeColor="#000" OnClick="lbtnPutSameValue_Click" CssClass="btn btn-primary primarygrdBtn">Put Same Value</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvSection" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>

                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                   
                                </asp:TemplateField>--%>





                                <asp:TemplateField HeaderText="Name &amp; Designation">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnkFiUpdate" runat="server" OnClick="lnkFiUpdate_Click" CssClass="btn  btn-success btn-sm">Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvndesig" runat="server" Text='<%#"<b>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))+"</b>"+"<br>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                            Width="160px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Card #">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvCardNo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>

                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Joining Date">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvjoidat" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joindate")).ToString("dd-MMM-yyyy") %>'
                                            Width="80px" Font-Size="11PX"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Confirmation <br/>Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblconfirmdat" runat="server" Style="text-align: left"
                                            Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "confirmdate")).Year==1900? "" :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "confirmdate")).ToString("dd-MMM-yyyy")) %>'
                                            Width="80px" Font-Size="11PX"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Years">
                                    <ItemTemplate>
                                        <asp:Label ID="lblyears" runat="server" Style="text-align: center"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "years")).ToString("#,##0;(#,##0); ") %>'
                                            Width="40px" Font-Size="11PX"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Months">
                                    <ItemTemplate>
                                        <asp:Label ID="lblmonths" runat="server" Style="text-align: center"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "months")).ToString("#,##0;(#,##0); ") %>'
                                            Width="40px" Font-Size="11PX"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Pre. Salary">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvpreamt" runat="server" Style="text-align: right; border-style: none;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "grossal")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFpresal" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Increment %">
                                    <ItemTemplate>
                                        <asp:TextBox ID="lgvincpercnt" runat="server" Font-Size="11px" BackColor="Transparent" BorderColor="#660033"
                                            BorderStyle="Solid" BorderWidth="0px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "inpercnt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>

                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Increment Amount">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvincamt" runat="server" Font-Size="11px" BackColor="Transparent" BorderColor="#660033"
                                            BorderStyle="Solid" BorderWidth="0px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "incamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFincamt" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Final Amount">
                                    <ItemTemplate>
                                        <asp:TextBox ID="lgvfinamount" runat="server" Font-Size="11px" BackColor="Transparent" BorderColor="#660033"
                                            BorderStyle="Solid" BorderWidth="0px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "finincamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:Label ID="lgvFfinincamt" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>


                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Revise Salary">
                                    <ItemTemplate>
                                        <asp:TextBox ID="lgvRevise" runat="server" Font-Size="11px" BackColor="Transparent" BorderColor="#660033"
                                            BorderStyle="Solid" BorderWidth="0px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "revisesal")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:Label ID="lgvFRevise" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>


                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                            </Columns>
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
