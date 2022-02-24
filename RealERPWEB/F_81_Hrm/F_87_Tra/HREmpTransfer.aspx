<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="HREmpTransfer.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_87_Tra.HREmpTransfer1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script>
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

            <div class="card card-fluid container-data">
                <div class="card-header mt-3 mb-0 pb-0">
                    <div class="row mb-0 pb-0">                      
                        <asp:Label ID="lblfrmdate" runat="server" CssClass="btn btn-sm btn-secsondary mr-2 col-1">Transfer Date</asp:Label>
                        <asp:TextBox ID="txtCurTransDate" runat="server" CssClass="form-control form-control-sm col-2 "></asp:TextBox>
                        <cc1:CalendarExtender ID="txtCurTransDate_CalendarExtender" runat="server" Format="dd.MM.yyyy" TargetControlID="txtCurTransDate"></cc1:CalendarExtender>

                        <asp:Label ID="trnsferNo" runat="server" CssClass=" btn btn-sm btn-secsondary col-1">Transfer No</asp:Label>
                        <asp:Label ID="lblCurTransNo1" runat="server" CssClass=" btn btn-sm btn-secsondary mr-2 col-1">Transfer No</asp:Label>
                        <asp:TextBox ID="txtCurTransNo2" runat="server" ReadOnly="true" CssClass="form-control form-control-sm col-1"></asp:TextBox>

                        <asp:LinkButton ID="lbtnOk" runat="server" OnClick="lbtnOk_Click" CssClass="btn btn-success btn-sm ml-1 col-1">Ok</asp:LinkButton>

                         <asp:LinkButton ID="lbtnPrevTransList" runat="server" CssClass="btn btn-sm btn-secsondary mr-2 col-1" OnClick="lbtnPrevTransList_Click">Prev. Trans List:</asp:LinkButton>
                        <asp:DropDownList ID="ddlPrevISSList" data-placeholder="Choose previous.." runat="server" CssClass="chzn-select form-control col-2 ">
                        </asp:DropDownList>
                    </div>

                    <div class="row mb-0 pb-0 mt-2">
                        <div class="col-6 well" id="pnlCompany" runat="server" visible="False">


                            <asp:Label ID="lcomp" runat="server" CssClass="btn btn-sm btn-secsondary mr-2">From Company</asp:Label>
                            <asp:DropDownList ID="ddlCompany" data-placeholder="Choose Company.." runat="server" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" CssClass="chzn-select form-control" AutoPostBack="true">
                            </asp:DropDownList>

                            <asp:Label ID="lbldep" runat="server" CssClass="btn btn-sm btn-secsondary mr-2 ">Department</asp:Label>
                            <asp:DropDownList ID="ddlDepartment" data-placeholder="Choose Department.." runat="server" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true" CssClass="chzn-select form-control">
                            </asp:DropDownList>

                            <asp:Label ID="Label3" runat="server" CssClass="btn btn-sm btn-secsondary mr-2 ">Section</asp:Label>
                            <asp:DropDownList ID="ddlprjlistfrom" runat="server" CssClass="chzn-select form-control" OnSelectedIndexChanged="ddlprjlistfrom_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                            </asp:DropDownList>
                            <asp:Label ID="Label5" runat="server" CssClass="btn btn-sm btn-secsondary mr-2">Employee</asp:Label>
                            <asp:DropDownList ID="ddlEmpList" data-placeholder="Choose Employee.." runat="server"
                                CssClass="chzn-select form-control" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>


                        <div class="col-6 well " id="pnlToCompany" runat="server" visible="False">

                            <asp:Label ID="Label1" runat="server" CssClass="btn btn-sm btn-secsondary mr-2">To Company</asp:Label>
                            <asp:DropDownList ID="ddlToCompany" data-placeholder="Choose Company.." runat="server" OnSelectedIndexChanged="ddlToCompany_SelectedIndexChanged" CssClass="chzn-select form-control" AutoPostBack="true">
                            </asp:DropDownList>


                            <asp:Label ID="Label2" runat="server" CssClass="btn btn-sm btn-secsondary mr-2 ">Department</asp:Label>
                            <asp:DropDownList ID="ddlTodept" data-placeholder="Choose ToDepartment.." runat="server" OnSelectedIndexChanged="ddlTodept_SelectedIndexChanged" AutoPostBack="true" CssClass="chzn-select form-control">
                            </asp:DropDownList>


                            <asp:Label ID="Label4" runat="server" CssClass="btn btn-sm btn-secsondary mr-2 ">Section</asp:Label>
                            <asp:DropDownList ID="ddlprjlistto" data-placeholder="Choose Department.." runat="server" CssClass="chzn-select form-control" AutoPostBack="true">
                            </asp:DropDownList>

                            <div class="col-12 row mb-0 pb-0 mt-2">
                                <asp:TextBox ID="txtEmpDesignation" runat="server" CssClass="form-control form-control-sm col-3"></asp:TextBox>

                                <asp:Label ID="lblResList0" runat="server" CssClass="btn btn-sm btn-secsondary  col-2">Present At Place</asp:Label>
                                <asp:TextBox ID="txtpatplacedate" runat="server" CssClass="form-control form-control-sm col-2 mr-2"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtpatplacedate_CalendarExtender" runat="server"
                                    Format="dd.MM.yyyy" TargetControlID="txtpatplacedate"></cc1:CalendarExtender>
                                <asp:RadioButtonList ID="rbtTrnstype" runat="server" CssClass="form-control form-control-sm col-3 mr-2" RepeatColumns="6" RepeatDirection="Horizontal">
                                    <asp:ListItem>Type 1</asp:ListItem>
                                    <asp:ListItem>Type 2</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:LinkButton ID="lnkselect" runat="server" CssClass="btn btn-success btn-sm  col-1" OnClick="lnkselect_Click">Select</asp:LinkButton>
                            </div>

                        </div>

                    </div>

                </div>
                <div class="card-body">
                    <div class="row">
                        <asp:GridView ID="grvacc" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid0" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Card #">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvidcardno" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvempname" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnkupdate" runat="server" CssClass="btn btn-success btn-sm "
                                            ForeColor="#000" OnClick="lnkupdate_Click">Final Update</asp:LinkButton>
                                    </FooterTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvdesig" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="From Company">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvtfCompany" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tfcomdesc")) %>'></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="From Section">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvtfprjdesc" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tfprjdesc")) %>'></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="To Company">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvtCompany" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ttcomdesc")) %>'></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="To Section">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvttprjdesc" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ttprjdesc")) %>'
                                            ></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Present At Place">
                                    <ItemTemplate>
                                        <asp:Label ID="txtpatplace" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "pplacedate")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvremarks" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rmrks")) %>'
                                            BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                    </ItemTemplate>

                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </div>


                    <div id="pnlremarks" runat="server" visible="False">
                        <div class="row mb-0 pb-0 mt-2">
                            <asp:Label runat="server" class="btn btn-sm btn-secsondary  col-2" ID="ContentPlaceHolder1_Label6">Information of Finalcial matters</asp:Label>
                            <asp:TextBox class="form-control col-10" runat="server" TabIndex="17" ID="txtfmaters" TextMode="MultiLine" cols="20" Rows="2"></asp:TextBox>
                        </div>

                        <div class=" row mb-0 pb-0 mt-2">
                            <asp:Label runat="server" class="btn btn-sm btn-secsondary col-2" ID="lbl1">Special Note</asp:Label>
                            <asp:TextBox class="form-control col-10" runat="server" ID="txtspnote" TextMode="MultiLine" cols="20" Rows="2"></asp:TextBox>
                        </div>

                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
