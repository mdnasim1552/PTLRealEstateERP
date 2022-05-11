<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="HREmpTransferSpecial.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_87_Tra.HREmpTransferSpecial" %>

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

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });


        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
            var gvEmpListTrans = $('#<%=this.gvEmpListTrans.ClientID %>');
            gvEmpListTrans.Scrollable();

            $('.chzn-select').chosen({ search_contains: true });

        };
        function filter2(phrase, _id) {
            var words = phrase.value.toLowerCase().split(" ");
            var table = document.getElementById(_id);
            var ele;
            for (var r = 1; r <= table.rows.length; r++) {
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
                        <div class="col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblfrmdate" runat="server">Transfer Date</asp:Label>
                                <asp:TextBox ID="txtCurTransDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtCurTransDate_CalendarExtender" runat="server" Format="dd.MM.yyyy" TargetControlID="txtCurTransDate"></cc1:CalendarExtender>

                            </div>
                        </div>
                        <div class="col-lg-1">
                            <div class="form-group">

                                <asp:Label ID="lblCurTransNo1" runat="server" CssClass=" smLbl_to">Transfer No</asp:Label>
                                <asp:TextBox ID="txtCurTransNo2" runat="server" ReadOnly="true" CssClass="form-control form-control form-control-sm"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                            <asp:Label ID="Label4" runat="server">Company</asp:Label>
                            <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control  chzn-select" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                            </asp:DropDownList>
          </div>
                        </div>
                        <div class="col-lg-3">
                             <div class="form-group">
                            <asp:Label ID="lblDept" runat="server">Department</asp:Label>
                            <asp:DropDownList ID="ddlProjectName" runat="server" Width="336px" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control  chzn-select" TabIndex="6">
                            </asp:DropDownList>
                        </div>
                        </div>
                        <div class="col-lg-2">
                                   <div class="form-group">
                                                        <asp:Label ID="Label3" runat="server" CssClass="lblTxt lblName">Section</asp:Label>
                     
                                            <asp:DropDownList ID="ddlSection" runat="server" CssClass="form-control inputTxt chzn-select" TabIndex="6" AutoPostBack="true">
                                            </asp:DropDownList>
                        </div>
                        </div>
                                          <div class="col-lg-1">
                     <div class="form-group">
                                     <asp:Label ID="Label2" runat="server" >ID Card</asp:Label>
                         <input name="txtTerm" onkeyup="filter2(this, '<%=gvEmpListTrans.ClientID %>')" type="text" class="form-control form-control-sm" placeholder="Search here">
                 </div>
                           </div>

                        <div class="col-lg-1">
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm mt20" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                        </div>
                    </div>
                </div>
                <div class="card-body">
            <asp:GridView ID="gvEmpListTrans" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea"
                AutoGenerateColumns="False"
                ShowFooter="True" Width="420px">
                <RowStyle />
                <Columns>
                    <asp:TemplateField HeaderText="Sl.No.">
                        <ItemTemplate>
                            <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px"
                                Style="text-align: right"
                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Company Name">

                        <HeaderTemplate>

                            <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                Text="Company Name" Width="150px"></asp:Label>


                            <asp:HyperLink ID="hlbtntbCdataExcelemplist" runat="server"
                                CssClass="btn  btn-success btn-xs" ToolTip="Export Excel"><i  class=" fa fa-file-excel-o "></i></asp:HyperLink>

                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:Label ID="lgvcustname" runat="server"
                                Text='<%# "<B>" +Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname"))+"<B>" %>'
                                Width="200px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lgvTotalnagad" runat="server" Font-Bold="True" Font-Size="12px"
                                ForeColor="#000" Style="text-align: right"></asp:Label>
                        </FooterTemplate>


                        <ItemStyle HorizontalAlign="left" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
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
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Section Name">
                        <ItemTemplate>
                            <asp:Label ID="lblgvsectionname" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tfprjdesc")) %>'
                                Width="130px"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Card #">
                        <ItemTemplate>
                            <asp:Label ID="lblgvcardnoemp" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                Width="80px"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Employee Name">
                        <ItemTemplate>
                            <asp:Label ID="lblgvempname" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                Width="160px"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Designation">
                        <ItemTemplate>
                            <asp:Label ID="lblgvdesignationemp" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                Width="120px"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkId" runat="server"
                                Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkmerge"))=="True" %>'
                                Width="20px" />
                        </ItemTemplate>

                        <FooterTemplate>
                            <asp:LinkButton ID="lnkbtnApproved" runat="server"
                                OnClick="lnkbtnApproved_Click" ToolTip="Approved"> <span class=" fa fa-check "></span> </asp:LinkButton>

                        </FooterTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
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


            <%--             <fieldset class="scheduler-border fieldset_A">
                                <div class="form-horizontal">

                                    <div class="form-group">
                                        <div class="col-md-7 pading5px">
                                            <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName">Transfer Date</asp:Label>
                                            <asp:TextBox ID="txtCurTransDate" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtCurTransDate_CalendarExtender" runat="server" Format="dd.MM.yyyy" TargetControlID="txtCurTransDate"></cc1:CalendarExtender>

                                            <asp:Label ID="trnsferNo" runat="server" CssClass=" smLbl_to">Transfer No</asp:Label>
                                            <asp:Label ID="lblCurTransNo1" runat="server" CssClass=" smLbl_to">Transfer No</asp:Label>
                                            <asp:TextBox ID="txtCurTransNo2" runat="server" ReadOnly="true" CssClass=" inputDateBox "></asp:TextBox>


                                        </div>

                                    </div>


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


            <div class="col-sm-4">
                <asp:Panel ID="pnlToCompany" runat="server" CssClass="well padingLeft5px" Visible="false">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <asp:Label ID="Label5" runat="server" CssClass="btn btn-success primaryBtn"
                                Text="Transfer To :"></asp:Label>
                        </div>
                        <div class="form-group">

                            <div class="col-md-3 pading5px asitCol4">
                                <asp:Label ID="Label6" runat="server" CssClass="lblTxt lblName">Company</asp:Label>
                                <asp:DropDownList ID="ddlToCompany" runat="server" CssClass="form-control inputTxt chzn-select" Width="230px" OnSelectedIndexChanged="ddlToCompany_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="col-md-3 pading5px asitCol4">
                                <asp:Label ID="Label7" runat="server" CssClass="lblTxt lblName">Department</asp:Label>
                                <asp:DropDownList ID="ddlToProject" runat="server" Width="230px" OnSelectedIndexChanged="ddlToProject_SelectedIndexChanged" CssClass="form-control inputTxt chzn-select" TabIndex="2" AutoPostBack="true">
                                </asp:DropDownList>
                                <%--<asp:TextBox ID="txtSrchToSection" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>--%>
                                <%-- <asp:LinkButton ID="imgbtnToSection" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnToSection_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>--%>
                            </div>

                            <%-- <div class="col-sm-1 pading5px">
                                            <asp:LinkButton ID="lnkselect" runat="server" CssClass="btn btn-primary primaryBtn pull-right" OnClick="lnkselect_Click" TabIndex="26">Select</asp:LinkButton>
                                        </div>--%>
                        </div>

                        <div class="form-group">

                            <div class="col-md-3 pading5px asitCol4">
                                <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Section</asp:Label>
                                <asp:DropDownList ID="ddlToSection" runat="server" Width="230px" OnSelectedIndexChanged="ddlToSection_SelectedIndexChanged" CssClass="form-control inputTxt chzn-select" TabIndex="2" AutoPostBack="true">
                                </asp:DropDownList>

                            </div>
                            <div class="col-sm-1 pading5px">

                                <asp:LinkButton runat="server" ID="lnkbtnTrnserTo" type="submit" CssClass="btn btn-sm btn-success" OnClick="lnkbtnTrnserTo_Click"> Update</asp:LinkButton>
                                <%-- <asp:LinkButton ID="lnkselect" runat="server" CssClass="btn btn-primary primaryBtn pull-right" OnClick="lnkselect_Click" TabIndex="26">Select</asp:LinkButton>--%>
                            </div>
                        </div>


                        <%--   <div class="form-group" style="margin-bottom:8px;">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblResList0" runat="server" CssClass="lblTxt lblName">Present At Place</asp:Label>
                                            <asp:TextBox ID="txtpatplacedate" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtpatplacedate_CalendarExtender" runat="server"
                                                Format="dd.MM.yyyy" TargetControlID="txtpatplacedate">
                                            </cc1:CalendarExtender>
                                        </div>
                                        <div class="col-md-4 pading5px">
                                            <asp:RadioButtonList ID="rbtTrnstype" runat="server" CssClass="btn rbtnList1 pading5px chkBoxControl" RepeatColumns="6" RepeatDirection="Horizontal"
                                                TabIndex="28" Width="156px">
                                                <asp:ListItem>Type 1</asp:ListItem>
                                                <asp:ListItem>Type 2</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="col-md-3 pull-right">
                                            <asp:Label ID="lblmsg" runat="server" Visible="false" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                        </div>
                                    </div>--%>
                    </div>

                </asp:Panel>
            </div>










            <%--       
                        <div class="col-md-3"> 
                            <a> jgjfjfjfjf</a>
                            <div id="emptrnafer" class="modal animated slideInTop " role="dialog" data-keyboard="false" data-backdrop="static">
                    <div class="modal-dialog">
                        <div class="modal-content  ">
                            <div class="modal-header">

                                <button type="button" style="background-color: red;" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-close"></span></button>
                                <h4 class="modal-title">
                                    <span class="fa fa-table"></span>Transfer To  </h4>
                            </div>
                            <div class="modal-body form-horizontal">
                                <div class="row-fluid">

                                    <div class="form-horizontal">
                                        <div class="row">
                                            <div class="form-group">
                                                <div class="col-md-3 field-label-responsive">
                                                    <label for="lblcompany" >Company Name </label>
                                                </div>
                                                <div class="col-md-9">


                                                    <asp:DropDownList ID="ddlToCompany" runat="server"   OnSelectedIndexChanged="ddlToCompany_SelectedIndexChanged"  CssClass="form-control chzn-select ddlclass" AutoPostBack="true">
                                                    </asp:DropDownList>

                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <div class="col-md-3 field-label-responsive">
                                                    <label for="lbldesptname">Department Name </label>
                                                </div>
                                                <div class="col-md-9">


                                                    <asp:DropDownList ID="ddlToProject" runat="server" OnSelectedIndexChanged="ddlToProject_SelectedIndexChanged" CssClass="form-control chzn-select ddlclass" AutoPostBack="true">
                                                    </asp:DropDownList>

                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <div class="col-md-3 field-label-responsive">
                                                    <label for="lblsection">Section Name </label>
                                                </div>
                                                <div class="col-md-9">


                                                    <asp:DropDownList ID="ddlToSection" runat="server" OnSelectedIndexChanged="ddlToSection_SelectedIndexChanged"  CssClass="form-control chzn-select ddlclass" AutoPostBack="true">
                                                    </asp:DropDownList>

                                                </div>
                                            </div>
                                        </div>

                                    </div>




                                </div>


                            </div>
                           <%-- <div class="modal-footer ">
                               <asp:LinkButton runat="server" ID="lnkbtnTrnserTo" type="submit" OnClientClick="CloseModalAssign();" CssClass="btn btn-sm btn-success" OnClick="lnkbtnTrnserTo_Click"><i class="fa fa-save" aria-hidden="true"></i> Update</asp:LinkButton>


                            </div>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

