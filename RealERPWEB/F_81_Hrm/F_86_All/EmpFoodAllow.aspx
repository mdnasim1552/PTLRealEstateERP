<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="EmpFoodAllow.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_86_All.EmpFoodAllow" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .mt20 {
            margin-top: 20px;
        }

        .chzn-container {
            width: 100% !important;
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

        function filter2(phrase, _id) {
            var words = phrase.value.toLowerCase().split(" ");
            var table = document.getElementById(_id);
            var ele;
            for (var r = 0; r < table.rows.length; r++) {
                ele = table.rows[r].innerHTML.replace(/<[^>]+>/g, "");
                var displayStyle = 'none';
                for (var i = 0; i < words.length; i++) {
                    if (ele.toLowerCase().indexOf(words[i]) >= 0)
                        displayStyle = '';
                    else {
                        displayStyle = 'none';
                        break;
                    }
                }
                table.rows[r].style.display = displayStyle;
            }
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
                        <div class="col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lbldate" runat="server">Date</asp:Label>

                                <asp:DropDownList ID="ddlyearmon" runat="server" CssClass="form-control form-control-sm">
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Company</asp:Label>
                                <asp:DropDownList ID="ddlCompanyName" runat="server" Width="233" CssClass="form-control chzn-select" AutoPostBack="true" TabIndex="2">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                 
                                        <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName">Department</asp:Label>
                                        <asp:DropDownList ID="ddlDepartment" runat="server" Width="233" CssClass="form-control chzn-select" TabIndex="7">
                                        </asp:DropDownList>
                               
                                </div>
                            </div>
                        <div class="col-lg-1">
                                     <asp:Label ID="Label3" runat="server">Code 
                                        <asp:LinkButton ID="imgbtnSearchEmployee" runat="server"  OnClick="imgbtnSearchEmployee_Click" ><i class="fa fa-search"> </i></asp:LinkButton>

                                     </asp:Label>
                                        <asp:TextBox ID="txtSrcEmployee" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                        </div>

                                                                                          <div class="col-lg-1">
                     <div class="form-group">
                                     <asp:Label ID="Label4" runat="server" >ID Card</asp:Label>
                         <input name="txtTerm" onkeyup="filter2(this, '<%=gvEmpFAllow.ClientID %>')" type="text" class="form-control form-control-sm" placeholder="Search here">
                 </div>
                           </div>

                         <div class="col-lg-1">
				<div class="form-group">
                             <asp:Label ID="lblPage" runat="server">Page Size</asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" >
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

                         <div class="col-lg-1">
                     <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary btn-sm mt20" Text="Ok" OnClick="lnkbtnShow_Click1"></asp:LinkButton>
	

                    </div>
                </div>
              
            </div>
                  <div class="card-body">
                    <asp:GridView ID="gvEmpFAllow" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                        AutoGenerateColumns="False"
                        ShowFooter="True" OnRowDeleting="gvEmpFAllow_RowDeleting" OnPageIndexChanging="gvEmpFAllow_PageIndexChanging">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <%-- <asp:CommandField ShowDeleteButton="True" />--%>
                            <asp:TemplateField HeaderText="Emp ID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lgvEmpId" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                        Width="180px" Font-Bold="True" Font-Size="11px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Section">
                                <ItemTemplate>
                                    <asp:Label ID="lgvSectionmb" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                        Width="180px" Font-Bold="True" Font-Size="11px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Card #">
                                <FooterTemplate>
                                    <asp:LinkButton ID="lbtnTotalFoodBill" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lbtnTotalFoodBill_Click">Total</asp:LinkButton>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvCardno" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                        Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Employee Name & Designation">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvEmpName" runat="server"
                                        Text='<%#"<b>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))+"</b >"+"<br />"+Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                        Width="220px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lbntUpdateFbill" runat="server" CssClass="btn btn-success btn-sm" OnClick="lbntUpdateFbill_Click">Update</asp:LinkButton>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Food Allowance">

                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvfbill" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mfoodamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="120px" Font-Size="11px"></asp:TextBox>
                                </ItemTemplate>

                                <FooterTemplate>
                                    <asp:Label ID="lgvFFoodbillamt" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="#000" Style="text-align: right" Width="120px"></asp:Label>
                                </FooterTemplate>

                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Other" Visible="false">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvothbill" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "othbill")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="80px" Font-Size="11px"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFFbillothamt" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Remarks">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvRemarks" runat="server" BorderStyle="None"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                        Width="120px" Font-Bold="True" Font-Size="11px"></asp:TextBox>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
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

            <%-- <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lbldate" runat="server" CssClass="lblTxt lblName">Date</asp:Label>

                                        <asp:DropDownList ID="ddlyearmon"  runat="server">
                                           <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Company</asp:Label>
                                        <asp:TextBox ID="txtSrcCompany" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnFindDepartment" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnFindDepartment_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlCompanyName" runat="server" Width="233" CssClass="form-control inputTxt pull-left"  AutoPostBack="true" TabIndex="2">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblCompanyName" runat="server" CssClass="dataLblview" Visible="False" Width="233px"></asp:Label>
                                        <div class="pull-left">
                                            <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary okBtn pull-left" Text="Ok" OnClick="lnkbtnShow_Click1"></asp:LinkButton>
                                        </div>
                                    </div>

                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName">Department</asp:Label>
                                        <asp:TextBox ID="txtSrcDepartment" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnDeptSrch" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnDeptSrch_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-3 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlDepartment" runat="server" Width="233" CssClass="form-control inputTxt" TabIndex="7">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblDeptDesc" CssClass="dataLblview" runat="server" Visible="False" Width="233"></asp:Label>
                                    </div>

                                </div>
                                <div class="form-group">
                                   <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName">Page Size</asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" >
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
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label3" runat="server" CssClass=" smLbl">Code</asp:Label>
                                        <asp:TextBox ID="txtSrcEmployee" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnSearchEmployee" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnSearchEmployee_Click" ><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>


                                         

                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                    </div>

                                </div>
                            </div>
                        </fieldset>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

