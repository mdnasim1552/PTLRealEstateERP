<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="EmpPerAppraisal.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_91_ACR.EmpPerAppraisal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


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
            var gv = $('#<%=this.gvPerAppraisal.ClientID %>');
            gv.Scrollable();

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

                    <fieldset class="scheduler-border fieldset_A">

                        <div class=" row">
                            <div class="col-lg-1 col-md-1 col-sm-2 ">
                                <div class="form-group">
                                    <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Ref No.</asp:Label>
                                    <asp:TextBox ID="txtrefno" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-2 col-md-4 col-sm-4 ">
                                <div class="form-group">
                                    <asp:Label ID="Label6" runat="server" CssClass=" smLbl_to"> Date</asp:Label>
                                    <asp:TextBox ID="txtCurDate" runat="server" CssClass="form-control"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server"
                                        Format="dd-MMM-yyyy" TargetControlID="txtCurDate"></cc1:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-lg-1 col-md-2 col-sm-4 ">
                                <div class="form-group">
                                    <asp:Label ID="lblRefNo2" runat="server" CssClass="smLbl">ID</asp:Label>
                                    <asp:Label ID="lblCurNo1" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-lg-1 col-md-2 col-sm-4 mt-4">
                                <div class="form-group">
                                    <asp:Label ID="lblCurNo2" runat="server" CssClass="form-control"></asp:Label>

                                </div>
                            </div>


                            <div class="col-md-2 pull-right">

                                <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn" Visible="false"></asp:Label>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-lg-1 col-md-3 col-sm-3">

                                <div class="form-group">
                                    <asp:Label ID="Label5" runat="server" CssClass="lblTxt lblName">Company
                                             <asp:LinkButton ID="ibtnFindCompanyApr" runat="server" OnClick="ibtnEmpList_Click"><span class="fas fa-search"> </span></asp:LinkButton>
                                    </asp:Label>
                                    <asp:TextBox ID="textSrcCompanyApr" runat="server" CssClass="form-control"></asp:TextBox>

                                </div>
                            </div>
                            <div class="col-lg-2 col-md-4 col-sm-4 mt-4">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlCompanyApr" runat="server" CssClass=" chzn-select form-control inputTxt">
                                    </asp:DropDownList>
                                    <asp:Label ID="lblCompanyNameAgg" runat="server" CssClass="form-control dataLblview" Visible="false"></asp:Label>
                                </div>
                            </div>

                            <div class="col-lg-2 col-md-2 pull-left mt-4">
                                <div class="form-group">
                                    <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                </div>
                            </div>
                            <div class="col-md-2 pull-left">
                                <div class="form-group">
                                    <asp:LinkButton ID="lbtnshow" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnshow_Click" Visible="false">Show</asp:LinkButton>
                                </div>
                            </div>
                        </div>

                        <div class="row">

                            <div class="col-lg-1 col-md-3 ">
                                <div class="form-group row">
                                    <asp:Label ID="lbldeptnameapr" runat="server" CssClass="lblTxt lblName">Department
                                            <asp:LinkButton ID="lbtndeptapr" runat="server" OnClick="lbtndeptapr_Click"><span class="fas fa-search"> </span></asp:LinkButton>
                                    </asp:Label>
                                    <asp:TextBox ID="txtsrchdeptapr" runat="server" CssClass="form-control"></asp:TextBox>

                                </div>
                            </div>
                            <div class=" col-lg-2 col-md-3  col-sm-4 mt-4">
                                <div class="form-group ">
                                    <asp:DropDownList ID="ddldepartmentapr" runat="server" CssClass=" chzn-select form-control inputTxt" OnSelectedIndexChanged="ddldepartmentapr_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:Label ID="lblvaldeptapr" runat="server" CssClass="form-control dataLblview" Visible="false"></asp:Label>
                                </div>
                            </div>

                            <div class="col-lg-2 col-md-2 ">
                                <div class="form-group ">
                                    <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName">Employee List 
                                             <asp:LinkButton ID="ibtnEmpList" runat="server" OnClick="ibtnEmpList_Click"><span class="fas fa-search"> </span></asp:LinkButton>
                                    </asp:Label>
                                    <asp:TextBox ID="txtEmpSrc" runat="server" CssClass="form-control"></asp:TextBox>

                                </div>
                            </div>
                            <div class=" col-lg-2 col-md-3 col-sm-4 mt-4">
                                <div class="form-group ">
                                    <asp:DropDownList ID="ddlEmpName" runat="server" CssClass=" chzn-select form-control inputTxt">
                                    </asp:DropDownList>
                                    <asp:Label ID="lblEmpname" runat="server" CssClass="form-control dataLblview" Visible="false"></asp:Label>
                                </div>

                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-1 col-md-3 ">
                                <div class="form-group">

                                    <asp:Label ID="lblsection" runat="server" CssClass="lblTxt lblName">Section
                                            <asp:LinkButton ID="ibtnFindProject" runat="server" OnClick="ibtnFindProject_Click"><span class="fas fa-search"> </span></asp:LinkButton>
                                    </asp:Label>
                                    <asp:TextBox ID="txtSrcPro" runat="server" CssClass="form-control"></asp:TextBox>

                                </div>
                            </div>
                            <div class="col-lg-2 col-md-3 col-sm-4 mt-4">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlProjectName" runat="server" CssClass=" chzn-select form-control " OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:Label ID="lblProjectdesc" runat="server" CssClass="form-control dataLblview" Visible="false"></asp:Label>
                                </div>
                            </div>
                            <div class="col-lg-2 col-md-3 ">
                                <div class="form-group">
                                    <asp:Label ID="lblprelist" runat="server" CssClass="lblTxt lblName">Pre. Appraisal
                                            <asp:LinkButton ID="ibtnPreList" runat="server" OnClick="ibtnPreList_Click"><span class="fas fa-search"> </span></asp:LinkButton>
                                    </asp:Label>
                                    <asp:TextBox ID="txtPreViousList" runat="server" CssClass="form-control"></asp:TextBox>

                                </div>
                            </div>
                            <div class="col-lg-2 col-md-3 col-sm-4 mt-4">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlPreList" runat="server" CssClass=" chzn-select form-control " AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:Label ID="Label3" runat="server" CssClass="form-control dataLblview" Visible="false"></asp:Label>
                                </div>
                                <%--<div class="col-md-2 pull-right">
                                        <asp:LinkButton ID="lbtnshow" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnshow_Click" Visible="false">Show</asp:LinkButton>
                                    </div>--%>
                            </div>
                        </div>

                    </fieldset>
                   
                        <fieldset class="scheduler-border fieldset_D">
                             <div class="row">
                                  <div class=" col-lg-4 col-md-8 ">
                                <div class="form-group">
                                  
                                        <div class="input-group">
                                            <span class="input-group-addon glypingraddon">
                                                <asp:Label ID="lblNarration" runat="server" CssClass="lblTxt" Text="Narration:"></asp:Label>
                                            </span>
                                            <asp:TextBox ID="txtNarr" runat="server" class="form-control" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="clearfix"></div>
                                </div>

                           </div>

                        </fieldset>
                   
                </div>
                <div class="card-body">
                    <div class="table-responsive">
                        <asp:GridView ID="gvPerAppraisal" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" Width="1101px">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Description ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDescription" runat="server"
                                            Text='<%#"<b>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc"))+"</b>"+"<br />"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "dgdesc")) %>'
                                            Width="200px" />
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnUpPerAppraisal" runat="server" Font-Bold="True"
                                            CssClass="btn btn-primary btn-sm primaryBtn" OnClick="lbtnUpPerAppraisal_Click">Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>





                                <asp:TemplateField HeaderText="1">
                                    <ItemTemplate>

                                        <%--<input id="nonunionexempt" type="checkbox" value="0" name="employeeType"  title="2 Mark" tabindex="8" runat="server"/>
                                        <input id="Checkbox1" type="checkbox" value="1" name="employeeType" tabindex="8" runat="server" />--%>


                                        <%--    <asp:RadioButtonList ID="list1" runat="server"  ForeColor="black"  BorderStyle="None" CssClass="btn btn-primary" style="border:hidden !important"  RepeatDirection="Horizontal" >
                                       <asp:ListItem Value="2">  2 Mark </asp:ListItem>
                                        <asp:ListItem Value="3"> 3 Mark </asp:ListItem>

                                       </asp:RadioButtonList>--%>

                                        <asp:CheckBox ID="lblgvsgval1" runat="server"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sgval1"))=="True"  %>'
                                            Text='<%#"<b>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "smgdesc1"))+"</b>"+"<br />"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "sgdesc1")) %>'
                                            Width="50px" />
                                        <br></br>

                                        <%--  <asp:CheckBox ID="list1"  runat="server" Visible="false"  value="1"  Text=" 1 Mark"/>
                                  <asp:CheckBox ID="list2"  runat="server"  Visible="false" value="2"  Text=" 2 Mark"/>
                                  <asp:CheckBox ID="list3"  runat="server"   Visible="false" value="3"  Text=" 3 Mark"/>--%>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:LinkButton ID="btndeleteApprisal" runat="server" Font-Bold="True"
                                            CssClass="btn btn-danger btn-sm primaryBtn" OnClick="btndeleteApprisal_Click" Visible="false">Delete ALL </asp:LinkButton>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="2">
                                    <ItemTemplate>

                                        <%--       <asp:RadioButtonList ID="list2" runat="server"  ForeColor="black"  BorderStyle="None" CssClass="btn btn-primary" style="border:hidden !important"  RepeatDirection="Horizontal"  >
                                       <asp:ListItem Value="4">  4 Mark </asp:ListItem>
                                        <asp:ListItem Value="5"> 5 Mark </asp:ListItem>
                                        <asp:ListItem Value="6"> 6 Mark </asp:ListItem>

                                         </asp:RadioButtonList>--%>
                                        <asp:CheckBox ID="lblgvsgval2" runat="server"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sgval2"))=="True" %>'
                                            Text='<%#"<b>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "smgdesc2"))+"</b>"+"<br />"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "sgdesc2")) %>'
                                            Width="50px" />
                                        <br></br>

                                        <%--                                        <asp:CheckBox ID="list4"  runat="server"  Visible="false" value="4"  Text=" 4 Mark"/>
                                        <asp:CheckBox ID="list5"  runat="server" Visible="false"  value="5"  Text=" 5 Mark"/>
                                        <asp:CheckBox ID="list6"  runat="server" Visible="false"  value="6"  Text=" 6 Mark"/>--%>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="3">
                                    <ItemTemplate>

                                        <%-- <asp:RadioButtonList ID="list3" runat="server" BorderStyle="None" CssClass="btn btn-primary" style="border:hidden !important"  RepeatDirection="Horizontal" >
                                       <asp:ListItem Value="7">  7 Mark </asp:ListItem>
                                        <asp:ListItem Value="8"> 8 Mark </asp:ListItem>                                        

                                         </asp:RadioButtonList>--%>

                                        <asp:CheckBox ID="lblgvsgval3" runat="server"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sgval3"))=="True" %>'
                                            Text='<%#"<b>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "smgdesc3"))+"</b>"+"<br />"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "sgdesc3")) %>'
                                            Width="50px" />
                                        <br></br>

                                        <%--    <asp:CheckBox ID="list7"  runat="server" Visible="false" value="7"  Text=" 7 Mark"/>
                                        <asp:CheckBox ID="list8"  runat="server" Visible="false" value="8"  Text=" 8 Mark"/>--%>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="4">
                                    <ItemTemplate>


                                        <asp:CheckBox ID="lblgvsgval4" runat="server"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sgval4"))=="True" %>'
                                            Text='<%#"<b>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "smgdesc4"))+"</b>"+"<br />"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "sgdesc4")) %>'
                                            Width="50px" />
                                        <br></br>

                                        <%-- <asp:CheckBox ID="list9"  runat="server" Visible="false"  value="9"  Text=" 9 Mark"/>
                                        <asp:CheckBox ID="list10"  runat="server" Visible="false"  value="8"  Text=" 10 Mark"/>--%>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="5">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="lblgvsgval5" runat="server"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sgval5"))=="True" %>'
                                            Text='<%#"<b>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "smgdesc5"))+"</b>"+"<br />"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "sgdesc5")) %>'
                                            Width="30px" />
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="6">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="lblgvsgval6" runat="server"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sgval6"))=="True" %>'
                                            Text='<%#"<b>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "smgdesc6"))+"</b>"+"<br />"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "sgdesc6")) %>'
                                            Width="30px" />
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="7">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="lblgvsgval7" runat="server"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sgval7"))=="True" %>'
                                            Text='<%#"<b>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "smgdesc7"))+"</b>"+"<br />"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "sgdesc7")) %>'
                                            Width="30px" />
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="8">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="lblgvsgval8" runat="server"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sgval8"))=="True" %>'
                                            Text='<%#"<b>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "smgdesc8"))+"</b>"+"<br />"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "sgdesc8")) %>'
                                            Width="30px" />
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="9">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="lblgvsgval9" runat="server"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sgval9"))=="True" %>'
                                            Text='<%#"<b>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "smgdesc9"))+"</b>"+"<br />"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "sgdesc9")) %>'
                                            Width="30px" />
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="10">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="lblgvsgval10" runat="server"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sgval10"))=="True" %>'
                                            Text='<%#"<b>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "smgdesc10"))+"</b>"+"<br />"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "sgdesc10")) %>'
                                            Width="30px" />
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



                </div>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


