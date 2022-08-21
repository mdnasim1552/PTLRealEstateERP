<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptIncrement.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_93_AnnInc.RptIncrement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .mt20 {
            margin-top: 20px;
        }
        div#ContentPlaceHolder1_ddlCompany_chzn{
            width:100%!important;
        }
        div#ContentPlaceHolder1_ddlDept_chzn{
              width:100%!important;
        }
        div#ContentPlaceHolder1_ddlSection_chzn{
              width:100%!important;
        }
        .chzn-drop{
      width:100%!important;
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

            var gvAnnIncre = $('#<%=this.gvAnnIncre.ClientID %>');
            gvAnnIncre.Scrollable();
            $('.chzn-select').chosen({ search_contains: true });
            $(".chosen-select").chosen({
                search_contains: true,
                no_results_text: "Sorry, no match!",
                allow_single_deselect: true
            });

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
                    <div class="row">
                        <div class="col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="lblCompany" runat="server">Company</asp:Label>
                                <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control  chzn-select" TabIndex="7" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>




                        <div class="col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="lblDept" runat="server">Department</asp:Label>
                                <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control  chzn-select" TabIndex="7" AutoPostBack="True" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>



                        <div class="col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="lblSection" runat="server">Department</asp:Label>
                                <asp:DropDownList ID="ddlSection" runat="server" CssClass="form-control  chzn-select"  AutoPostBack="True" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>


                        <div class="col-lg-2">

                            <div class="form-group">
                                <asp:Label ID="lblPage" runat="server">Page Size</asp:Label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
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



                    </div>

                    <div class="row">

                                <%--                       <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" RepeatDirection="Vertical" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                        <asp:ListItem Value="all">All</asp:ListItem>
                                        <asp:ListItem Value="approved">Approved</asp:ListItem>
                      
                                    </asp:RadioButtonList>--%>



                        <div class="col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblfrmdate" runat="server">From</asp:Label>
                                <asp:TextBox ID="txtfrmDate" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfrmDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfrmDate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lbltodate" runat="server">To</asp:Label>
                                <asp:TextBox ID="txttoDate" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="txttoDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttoDate"></cc1:CalendarExtender>

                            </div>
                        </div>


                        <div class="col-lg-1">
                            <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary mt20" OnClick="lnkbtnShow_Click">Ok</asp:LinkButton>
                        </div>
                                        <div class="col-lg-2 mt20">
                                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True"
                                            CssClass="custom-control custom-control-inline custom-checkbox rbt "
                                            Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged"
                                            RepeatDirection="Horizontal">
                                        <asp:ListItem Value="all">Pending</asp:ListItem>
                                        <asp:ListItem Value="approved">Approved</asp:ListItem>

                                        </asp:RadioButtonList>
                                    </div>



                    </div>
                    <div class="card-body">
                        <div class=" table table-responsive">
                            <div class="row">
                                <asp:GridView ID="gvAnnIncre" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    OnPageIndexChanging="gvAnnIncre_PageIndexChanging" ShowFooter="True" Width="831px" CssClass="table-striped table-hover table-bordered grvContentarea">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Increment No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvIncNo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "incrno1")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Increment Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvIncdate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "incrdate1")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="lgProName" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")) %>'
                                                    Width="120px" Font-Bold="True"></asp:Label>
                                            </ItemTemplate>

                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Section">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvSection" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Name &amp; Designation">

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

                                            <FooterTemplate>
                                                <asp:Label ID="lgvjoindat" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right"> Total :</asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Confirmation Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblconfirmdat" runat="server" Style="text-align: left"
                                                    Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "confirmdate")).Year==1900? "" :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "confirmdate")).ToString("dd-MMM-yyyy")) %>'
                                                    Width="80px" Font-Size="11PX"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Pre. Salary">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpreamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "grossal")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFGross" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
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
                                                <asp:Label ID="lgvFicreamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
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
                                                <asp:Label ID="lgvFfinincamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Gross Salary </br> After Inc.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtogross" runat="server" Font-Size="11px" BackColor="Transparent" BorderColor="#660033"
                                                    BorderStyle="Solid" BorderWidth="0px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tosalary")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtogross" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
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
                </div>


                <%--          <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName">Company</asp:Label>
                                        <asp:TextBox ID="txtSrcCompany" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnCompany" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnCompany_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlCompany" runat="server"  CssClass="form-control inputTxt pull-left" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblCompany" runat="server" BackColor="White" Visible="False" CssClass="form-control inputTxt"></asp:Label>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary okBtn pull-left" OnClick="lnkbtnShow_Click">Ok</asp:LinkButton>

                                    </div>

                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Department</asp:Label>
                                        <asp:TextBox ID="txtSrcDept" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnDeptSrch" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnDeptSrch_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control inputTxt pull-left" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblDept" runat="server" BackColor="White" Visible="False" CssClass="form-control inputTxt"></asp:Label>
                                    </div>


                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName">Section</asp:Label>
                                        <asp:TextBox ID="txtSrcSection" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnSectionSrch" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnSectionSrch_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlSection" runat="server" CssClass="form-control inputTxt pull-left" AutoPostBack="true" TabIndex="2">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblSection" runat="server" BackColor="White" Visible="False" CssClass="form-control inputTxt"></asp:Label>
                                    </div>


                                </div>

                                <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPage" runat="server" CssClass="smLbl_to">Page Size</asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" Width="81px" CssClass="ddlPage" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
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

                

                                 <div class="form-group">
                                    <div class="col-md-5 pading5px">
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName">Date</asp:Label>
                                        <asp:TextBox ID="txtfrmDate" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfrmDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfrmDate">
                                        </cc1:CalendarExtender>

                                        <asp:Label ID="lbltodate" runat="server" CssClass=" smLbl_to">To</asp:Label>
                                        <asp:TextBox ID="txttoDate" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttoDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttoDate">
                                        </cc1:CalendarExtender>
                                    </div>
                                </div>
                            </div>
                        </fieldset>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
