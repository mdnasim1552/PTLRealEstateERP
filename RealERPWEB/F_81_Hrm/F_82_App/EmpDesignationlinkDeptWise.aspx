<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="EmpDesignationlinkDeptWise.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_82_App.EmpDesignationlinkDeptWise" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <style>
        div#ContentPlaceHolder1_ddlOthersBook_chzn {
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
        div#ContentPlaceHolder1_ddlCompany_chzn{
              width: 100% !important;
        }
        div#ContentPlaceHolder1_ddlProjectName_chzn{
            width: 100% !important;
        }
        div#ContentPlaceHolder1_ddlSection_chzn{
            width: 100% !important;

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
                         <div class="col-lg-3">
                        <div class="form-gorup">
                            <asp:Label ID="Label4" runat="server">Company</asp:Label>
                            <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-lg-3">
                        <div class="form-gorup">
                            <asp:Label ID="lblDept" runat="server" >Department</asp:Label>
                            <asp:DropDownList ID="ddlProjectName" runat="server" Width="336px" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control  chzn-select" TabIndex="6">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-lg-3">
                        <div class="form-gorup">
                                 <asp:Label ID="Label3" runat="server" >Section</asp:Label>
                                  <asp:DropDownList ID="ddlSection" runat="server" CssClass="form-control  chzn-select" TabIndex="6" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-lg-1">
                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm mt20" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                    </div>
                    </div>
                </div>
                <div class="card-body">
            <asp:GridView ID="gvEmpListslno" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea"
                AutoGenerateColumns="False" OnPageIndexChanging="gvEmpListslno_PageIndexChanging" AllowPaging="true"  PageSize="3000"
                ShowFooter="True" Width="420px">
                <RowStyle />
                <Columns>
                    <asp:TemplateField HeaderText="Sl.">
                        <ItemTemplate>
                            <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="12px"
                                Style="text-align: right"
                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-size="16px" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Company Name">

                        <HeaderTemplate>

                            <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                Text="Company Name" Width="150px"></asp:Label>


                            <asp:HyperLink ID="hlbtntbCdataExcelemplist" runat="server"
                                CssClass="btn  btn-success btn-xs" ToolTip="Export Excel"><i class=" fa fa-file-excel-o "></i></asp:HyperLink>

                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:Label ID="lgvcustname" runat="server"
                                Text='<%# "<B>" +Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname"))+"<B>" %>'
                                Width="200px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lgvTotalnagad" runat="server" 
                                ForeColor="#000" Style="text-align: right"></asp:Label>
                        </FooterTemplate>


                        <ItemStyle HorizontalAlign="left" />
                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-size="16px" />
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="Company Name">



                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCompanynameemp" runat="server"
                                                    Text='<%# "<B>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname"))  +"<B>"  %>'
                                                    Width="150px">                                                                        
                                              
                                                </asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                        </asp:TemplateField>--%>

                    <asp:TemplateField HeaderText="Department Name">
                        <ItemTemplate>
                            <asp:Label ID="lblgvdepname" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptdesc")) %>'
                                Width="130px"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-size="16px" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Section Name">
                        <ItemTemplate>
                            <asp:Label ID="lblgvsectionname" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sectionname")) %>'
                                Width="150px"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-size="16px"/>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Card ">
                        <ItemTemplate>
                            <asp:Label ID="lblgvcardnoemp" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                Width="80px"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-size="16px" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Employee Name">
                        <ItemTemplate>
                            <asp:Label ID="lblgvempname" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                Width="160px"></asp:Label>
                        </ItemTemplate>

                        <FooterTemplate>
                            <asp:LinkButton ID="lbntUpdateSl" runat="server" OnClick="lbntUpdateSl_Click" CssClass="btn btn-success btn-sm">Update</asp:LinkButton>
                        </FooterTemplate>
                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-size="16px" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Designation">
                        <ItemTemplate>
                            <asp:Label ID="lblgvdesignationemp" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                Width="150px"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-size="16px" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Sl No">
                        <ItemTemplate>
                            <asp:TextBox ID="txtslno" runat="server"
                                Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "slno")).ToString("#,##0;(#,##0); ") %>'
                                Width="40px" BorderColor="blue" BorderWidth="1px" BackColor="White" BorderStyle="Solid" Style="text-align: center" Font-Bold="true"></asp:TextBox>
                        </ItemTemplate>
                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-size="16px" />
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

            <%--            <fieldset class="scheduler-border fieldset_A">
                <div class="form-horizontal">
                    <div class="form-group">
                        <div class="col-md-3 pading5px asitCol3">
                            <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName">Company</asp:Label>
                            <asp:TextBox ID="txtSrcCompany" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                            <asp:LinkButton ID="imgbtnCompany" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnCompany_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                        </div>
                        <div class="col-md-4 asitCol4 pading5px">
                            <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control inputTxt chzn-select" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                            </asp:DropDownList>

                        </div>
                        <div class="col-md-1 asitCol1 pading5px">
                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn pull-left" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                        </div>

                    </div>

                    <div class="form-group">
                        <div class="col-md-3 pading5px asitCol3">
                            <asp:Label ID="lblDept" runat="server" CssClass="lblTxt lblName">Department</asp:Label>
                            <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                            <asp:LinkButton ID="ibtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                        </div>
                        <div class="col-md-3 pading5px asitCol4">
                            <asp:DropDownList ID="ddlProjectName" runat="server" Width="336px" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control inputTxt chzn-select" TabIndex="6">
                            </asp:DropDownList>

                        </div>

                    </div>

                    <div class="form-group">
                        <div class="col-md-3 pading5px asitCol3">
                            <asp:Label ID="Label3" runat="server" CssClass="lblTxt lblName">Section</asp:Label>
                            <asp:TextBox ID="txtSrcSec" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                            <asp:LinkButton ID="imgbtnSecSrch" runat="server" CssClass="btn btn-primary srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                        </div>
                        <div class="col-md-4 pading5px asitCol4">
                            <asp:DropDownList ID="ddlSection" runat="server" CssClass="form-control inputTxt chzn-select" TabIndex="6" AutoPostBack="true">
                            </asp:DropDownList>

                            <cc1:ListSearchExtender ID="ddlSection_ListSearchExtender" runat="server"
                                QueryPattern="Contains" TargetControlID="ddlSection">
                            </cc1:ListSearchExtender>
                        </div>

                    </div>



                </div>
            </fieldset>--%>

























        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


