<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="PrjCompArchiaInterior.aspx.cs" Inherits="RealERPWEB.F_08_PPlan.PrjCompArchiaInterior" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .RowColor {
            color: maroon;
            font-size: 14px !important;
            font-family: Cambria;
        }
    </style>
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });


        function loadModal() {
            $('#AddComments').modal('toggle', {
                backdrop: 'static',
                keyboard: false
            });
        }


        function UsrloadModal() {
            $('#AddUser').modal('toggle', {
                backdrop: 'static',
                keyboard: false
            });
        }

        function DocloadModal() {
            $('#AddDoc').modal('toggle', {
                backdrop: 'static',
                keyboard: false
            });
        }


        function CloseModal() {
            $('#AddComments').modal('hide');
            $('#AddUser').modal('hide');
            $('#AddDoc').modal('hide');
        }


        function pageLoaded() {
            try {




                //$(".chosen-select").chosen({
                //    search_contains: true,
                //    no_results_text: "Sorry, no match!",
                //    allow_single_deselect: true
                //});
                //$('.chosen-continer').css({ 'width': '600px', "height": "20px" });


                $(".chosen-select").chosen({
                    search_contains: true,
                    no_results_text: "Sorry, no match!",
                    allow_single_deselect: true
                });
                $('.chosen-continer').css('width', '600px');


                $('.chzn-select').chosen({ search_contains: true });


            }

            catch (e) {

                alert(e.message);
            }

        }

    </script>

    <style>
        .switch {
            position: relative;
            display: inline-block;
            width: 40px;
            height: 20px;
        }

            .switch input {
                opacity: 0;
                width: 0;
                height: 0;
            }

        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 18px;
                width: 18px;
                left: 1px;
                bottom: 1px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
            }

        input:checked + .slider {
            background-color: #2196F3;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #2196F3;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(18px);
            -ms-transform: translateX(18px);
            transform: translateX(18px);
        }

        /* Rounded sliders */
        .slider.round {
            border-radius: 20px;
        }

            .slider.round:before {
                border-radius: 50%;
            }

        .form-group {
            margin-bottom: 0px !important;
        }
    </style>


    <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>

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
    <div class="card card-fluid" style="min-height: 450px;">
        <div class="card-body">
            <div class="row mt-2">

                 <div class="col-md-1">
                    <div class="form-group">
                        <label class="control-label  lblmargin-top9px ml-3" id="lblwork" runat="server">Work</label>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlwork" runat="server" CssClass="form-control chzn-select ddlPage">
                        </asp:DropDownList>
                    </div>
                </div>
                
                
                <div class="col-md-1">
                    <div class="form-group">
                        <label class="control-label  lblmargin-top9px" for="project" id="lblproject" runat="server">Project Name</label>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlPrjName" runat="server" CssClass=" form-control chzn-select">
                        </asp:DropDownList>
                    </div>
                </div>
               
                <div class="col-md-1">
                    <div class="form-group">
                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass=" btn btn-primary ml-1 btn-xs" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                    </div>
                </div>

            </div>

                 <%--<asp:Panel runat="server" ID="pnlJob" Visible="false">


                <div class="row">


                    <div class="col-md-1">
                        <div class="form-group">
                            <label class="control-label  lblmargin-top9px" id="lbljob" runat="server">Job</label>

                        </div>
                    </div>


                    <div class="col-md-8">
                        <div class="form-group">

                            <asp:ListBox ID="lstJob" runat="server" SelectionMode="Multiple" Style="width: 800px !important; height: 50px !important;"
                                data-placeholder="Choose Job......" multiple="true" class="form-control chosen-select"></asp:ListBox>



                        </div>
                    </div>

                    <div class="col-md-3">
                        <div class="form-group">
                            <asp:LinkButton ID="lbtnAdd" runat="server" CssClass=" btn btn-primary  " OnClick="lbtnAdd_Click">Add</asp:LinkButton>


                             <label id="chkbod" runat="server" class="switch">
                                    <asp:CheckBox ID="ChboxAll" runat="server" OnCheckedChanged="ChboxAll_CheckedChanged" AutoPostBack="true" />
                                    <span class="btn btn-xs slider round"></span>
                                </label>
                                <asp:Label runat="server" Text="All Job"  CssClass="btn btn-xs"></asp:Label>


                        </div>
                    </div>

                </div>

            </asp:Panel>--%>



            <asp:GridView ID="gvPrjInfo" runat="server" AutoGenerateColumns="False" CssClass="table-condensed table-hover table-bordered grvContentarea"
                ShowFooter="True" Style="margin-right: 0px" OnRowDataBound="gvPrjInfo_RowDataBound">
                <RowStyle />
                <Columns>
                    <asp:TemplateField HeaderText="Sl">
                        <ItemTemplate>
                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                Style="text-align: right"
                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Estimate Amount">

                       

                        <ItemTemplate>
                            <asp:TextBox ID="txtgvestamt" runat="server" BackColor="Transparent"
                                BorderStyle="None"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proesam")).ToString("#,##0;-#,##0; ")%>'
                                Width="60px" Style="text-align: right"></asp:TextBox>
                        </ItemTemplate>


                        <ItemStyle HorizontalAlign="right" />
                        <FooterStyle HorizontalAlign="Right" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Advanced Amount">

                        <FooterTemplate>
                            
                            <asp:LinkButton ID="lnkgvTotal" runat="server" CssClass="btn btn-primary btn-xs" OnClick="lnkgvTotal_Click">Total</asp:LinkButton>
                        </FooterTemplate>

                        <ItemTemplate>
                            <asp:TextBox ID="txtgvadvamt" runat="server" BackColor="Transparent"
                                BorderStyle="None"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proadam")).ToString("#,##0;-#,##0; ")%>'
                                Width="60px" Style="text-align: right"></asp:TextBox>
                        </ItemTemplate>


                        <ItemStyle HorizontalAlign="right" />
                        <FooterStyle HorizontalAlign="Right" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Due Amount">


                         <FooterTemplate>
                            
                            <asp:LinkButton ID="lnkgvUpdate" runat="server" CssClass="btn btn-primary btn-xs" OnClick="lnkgvUpdate_Click">Update</asp:LinkButton>
                        </FooterTemplate>


                        <ItemTemplate>
                            <asp:TextBox ID="txtgvdueamt" runat="server" BackColor="Transparent"
                                BorderStyle="None"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam")).ToString("#,##0;-#,##0; ")%>'
                                Width="60px" Style="text-align: right"></asp:TextBox>
                        </ItemTemplate>


                        <ItemStyle HorizontalAlign="right" />
                        <FooterStyle HorizontalAlign="Right" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="job1">
                        
                     
                        
                        <ItemTemplate>
                            <asp:Label ID="lblgvjob1" runat="server" BackColor="Transparent"
                                BorderStyle="None"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job1").ToString())%>'
                                Width="100px"></asp:Label>

                            <asp:TextBox ID="txtgvjob1" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job1")) %>'></asp:TextBox>



                            <asp:TextBox ID="txtgvjobd1" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job1")) %>'></asp:TextBox>
                            <cc1:CalendarExtender ID="txtgvjobd1_CalendarExtender" runat="server"
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvjobd1"></cc1:CalendarExtender>


                            <asp:DropDownList ID="ddlassignuser1" runat="server"   SelectionMode="Multiple" Style="width: 300px !important;"
                                                                data-placeholder="Choose Participant......" multiple="true" class="form-control chosen-select" CssClass=" inputTxt form-control"
                                TabIndex="12">
                            </asp:DropDownList>

                           


                        </ItemTemplate>
                        <FooterTemplate>

                            <asp:LinkButton ID="lbtnDeleteJob1" runat="server" OnClick="lbtnDeleteJob_Click"  CommandArgument="1" ToolTip="Remove Job"><i  style="color:red"  class=" fa fa-trash"></i></asp:LinkButton>

                        </FooterTemplate>
                      
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <FooterStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="job2">
                        <ItemTemplate>
                            <asp:Label ID="lblgvjob2" runat="server" BackColor="Transparent"
                                BorderStyle="None"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job2").ToString())%>'
                                Width="100px"></asp:Label>


                             <asp:TextBox ID="txtgvjob2" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job2")) %>'></asp:TextBox>



                            <asp:TextBox ID="txtgvjobd2" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job2")) %>'></asp:TextBox>
                            <cc1:CalendarExtender ID="txtgvjobd2_CalendarExtender" runat="server"
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvjobd2"></cc1:CalendarExtender>


                            <asp:DropDownList ID="ddlassignuser2" runat="server" CssClass=" inputTxt form-control"
                                TabIndex="12">
                            </asp:DropDownList>

                        </ItemTemplate>
                         <FooterTemplate>

                            <asp:LinkButton ID="lbtnDeleteJob2" runat="server" OnClick="lbtnDeleteJob_Click" CommandArgument="2" ToolTip="Remove Job"><i  style="color:red"  class=" fa fa-trash"></i></asp:LinkButton>

                        </FooterTemplate>
                      
                      <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="job3">
                        <ItemTemplate>
                            <asp:Label ID="lblgvjob3" runat="server" BackColor="Transparent"
                                BorderStyle="None"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job3").ToString())%>'
                                Width="100px"></asp:Label>

                             <asp:TextBox ID="txtgvjob3" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job3")) %>'></asp:TextBox>



                            <asp:TextBox ID="txtgvjobd3" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job3")) %>'></asp:TextBox>
                            <cc1:CalendarExtender ID="txtgvjobd3_CalendarExtender" runat="server"
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvjobd3"></cc1:CalendarExtender>


                            <asp:DropDownList ID="ddlassignuser3" runat="server" CssClass=" inputTxt form-control"
                                TabIndex="12">
                            </asp:DropDownList>
                        </ItemTemplate>


                         <FooterTemplate>

                            <asp:LinkButton ID="lbtnDeleteJob3" runat="server" OnClick="lbtnDeleteJob_Click" CommandArgument="3" ToolTip="Remove Job"><i  style="color:red"  class=" fa fa-trash"></i></asp:LinkButton>

                        </FooterTemplate>
                      
                      <FooterStyle HorizontalAlign="Center" />


                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="job4">
                        <ItemTemplate>
                            <asp:Label ID="lblgvjob4" runat="server" BackColor="Transparent"
                                BorderStyle="None"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job4").ToString())%>'
                                Width="100px"></asp:Label>

                             <asp:TextBox ID="txtgvjob4" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job4")) %>'></asp:TextBox>



                            <asp:TextBox ID="txtgvjobd4" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job4")) %>'></asp:TextBox>
                            <cc1:CalendarExtender ID="txtgvjobd4_CalendarExtender" runat="server"
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvjobd4"></cc1:CalendarExtender>


                            <asp:DropDownList ID="ddlassignuser4" runat="server" CssClass=" inputTxt form-control"
                                TabIndex="12">
                            </asp:DropDownList>

                        </ItemTemplate>
                         <FooterTemplate>

                            <asp:LinkButton ID="lbtnDeleteJob4" runat="server" OnClick="lbtnDeleteJob_Click" CommandArgument="4" ToolTip="Remove Job"><i  style="color:red"  class=" fa fa-trash"></i></asp:LinkButton>

                        </FooterTemplate>
                      
                      <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="job5">
                        <ItemTemplate>
                            <asp:Label ID="lblgvjob5" runat="server" BackColor="Transparent"
                                BorderStyle="None"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job5").ToString())%>'
                                Width="100px"></asp:Label>
                             <asp:TextBox ID="txtgvjob5" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job5")) %>'></asp:TextBox>



                            <asp:TextBox ID="txtgvjobd5" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job5")) %>'></asp:TextBox>
                            <cc1:CalendarExtender ID="txtgvjobd5_CalendarExtender" runat="server"
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvjobd5"></cc1:CalendarExtender>


                            <asp:DropDownList ID="ddlassignuser5" runat="server" CssClass=" inputTxt form-control"
                                TabIndex="12">
                            </asp:DropDownList>


                        </ItemTemplate>
                         <FooterTemplate>

                            <asp:LinkButton ID="lbtnDeleteJob5" runat="server" OnClick="lbtnDeleteJob_Click" CommandArgument="5" ToolTip="Remove Job"><i  style="color:red"  class=" fa fa-trash"></i></asp:LinkButton>

                        </FooterTemplate>
                      
                      <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="job6">
                        <ItemTemplate>
                            <asp:Label ID="lblgvjob6" runat="server" BackColor="Transparent"
                                BorderStyle="None"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job6").ToString())%>'
                                Width="100px"></asp:Label>

                             <asp:TextBox ID="txtgvjob6" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job6")) %>'></asp:TextBox>



                            <asp:TextBox ID="txtgvjobd6" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job6")) %>'></asp:TextBox>
                            <cc1:CalendarExtender ID="txtgvjobd6_CalendarExtender" runat="server"
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvjobd6"></cc1:CalendarExtender>


                            <asp:DropDownList ID="ddlassignuser6" runat="server" CssClass=" inputTxt form-control"
                                TabIndex="12">
                            </asp:DropDownList>
                        </ItemTemplate>
                         <FooterTemplate>

                            <asp:LinkButton ID="lbtnDeleteJob6" runat="server" OnClick="lbtnDeleteJob_Click" CommandArgument="6" ToolTip="Remove Job"><i  style="color:red"  class=" fa fa-trash"></i></asp:LinkButton>

                        </FooterTemplate>
                      
                      <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="job7">
                        <ItemTemplate>
                            <asp:Label ID="lblgvjob7" runat="server" BackColor="Transparent"
                                BorderStyle="None"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job7").ToString())%>'
                                Width="100px"></asp:Label>

                             <asp:TextBox ID="txtgvjob7" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job7")) %>'></asp:TextBox>



                            <asp:TextBox ID="txtgvjobd7" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job7")) %>'></asp:TextBox>
                            <cc1:CalendarExtender ID="txtgvjobd7_CalendarExtender" runat="server"
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvjobd7"></cc1:CalendarExtender>


                            <asp:DropDownList ID="ddlassignuser7" runat="server" CssClass=" inputTxt form-control"
                                TabIndex="12">
                            </asp:DropDownList>

                        </ItemTemplate>
                         <FooterTemplate>

                            <asp:LinkButton ID="lbtnDeleteJob7" runat="server" OnClick="lbtnDeleteJob_Click" CommandArgument="7" ToolTip="Remove Job"><i  style="color:red"  class=" fa fa-trash"></i></asp:LinkButton>

                        </FooterTemplate>
                      
                      <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="job8">
                        <ItemTemplate>
                            <asp:Label ID="lblgvjob8" runat="server" BackColor="Transparent"
                                BorderStyle="None"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job8").ToString())%>'
                                Width="100px"></asp:Label>

                             <asp:TextBox ID="txtgvjob8" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job8")) %>'></asp:TextBox>



                            <asp:TextBox ID="txtgvjobd8" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job8")) %>'></asp:TextBox>
                            <cc1:CalendarExtender ID="txtgvjobd8_CalendarExtender" runat="server"
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvjobd8"></cc1:CalendarExtender>


                            <asp:DropDownList ID="ddlassignuser8" runat="server" CssClass=" inputTxt form-control"
                                TabIndex="12">
                            </asp:DropDownList>

                        </ItemTemplate>
                         <FooterTemplate>

                            <asp:LinkButton ID="lbtnDeleteJob8" runat="server" OnClick="lbtnDeleteJob_Click" CommandArgument="8" ToolTip="Remove Job"><i  style="color:red"  class=" fa fa-trash"></i></asp:LinkButton>

                        </FooterTemplate>
                      
                      <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="job9">
                        <ItemTemplate>
                            <asp:Label ID="lblgvjob9" runat="server" BackColor="Transparent"
                                BorderStyle="None"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job9").ToString())%>'
                                Width="100px"></asp:Label>

                             <asp:TextBox ID="txtgvjob9" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job9")) %>'></asp:TextBox>



                            <asp:TextBox ID="txtgvjobd9" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job9")) %>'></asp:TextBox>
                            <cc1:CalendarExtender ID="txtgvjobd9_CalendarExtender" runat="server"
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvjobd9"></cc1:CalendarExtender>


                            <asp:DropDownList ID="ddlassignuser9" runat="server" CssClass=" inputTxt form-control"
                                TabIndex="12">
                            </asp:DropDownList>

                        </ItemTemplate>
                         <FooterTemplate>

                            <asp:LinkButton ID="lbtnDeleteJob9" runat="server" OnClick="lbtnDeleteJob_Click" CommandArgument="9" ToolTip="Remove Job"><i  style="color:red"  class=" fa fa-trash"></i></asp:LinkButton>

                        </FooterTemplate>
                      
                      <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="job10">
                        <ItemTemplate>
                            <asp:Label ID="lblgvjob10" runat="server" BackColor="Transparent"
                                BorderStyle="None"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job10").ToString())%>'
                                Width="80px"></asp:Label>

                             <asp:TextBox ID="txtgvjob10" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job10")) %>'></asp:TextBox>



                            <asp:TextBox ID="txtgvjobd10" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job10")) %>'></asp:TextBox>
                            <cc1:CalendarExtender ID="txtgvjobd10_CalendarExtender" runat="server"
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvjobd10"></cc1:CalendarExtender>


                            <asp:DropDownList ID="ddlassignuser10" runat="server" CssClass=" inputTxt form-control"
                                TabIndex="12">
                            </asp:DropDownList>

                        </ItemTemplate>
                         <FooterTemplate>

                            <asp:LinkButton ID="lbtnDeleteJob10" runat="server" OnClick="lbtnDeleteJob_Click" CommandArgument="10" ToolTip="Remove Job"><i  style="color:red"  class=" fa fa-trash"></i></asp:LinkButton>

                        </FooterTemplate>
                      
                      <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="job11">
                        <ItemTemplate>
                            <asp:Label ID="lblgvjob11" runat="server" BackColor="Transparent"
                                BorderStyle="None"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job11").ToString())%>'
                                Width="100px"></asp:Label>


                             <asp:TextBox ID="txtgvjob11" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job11")) %>'></asp:TextBox>
                            



                            <asp:TextBox ID="txtgvjobd11" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job11")) %>'></asp:TextBox>
                            <cc1:CalendarExtender ID="txtgvjobd11_CalendarExtender" runat="server"
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvjobd11"></cc1:CalendarExtender>


                            <asp:DropDownList ID="ddlassignuser11" runat="server" CssClass=" inputTxt form-control"
                                TabIndex="12">
                            </asp:DropDownList>
                        </ItemTemplate>
                         <FooterTemplate>

                            <asp:LinkButton ID="lbtnDeleteJob11" runat="server" OnClick="lbtnDeleteJob_Click"  CommandArgument="11" ToolTip="Remove Job"><i  style="color:red"  class=" fa fa-trash"></i></asp:LinkButton>

                        </FooterTemplate>
                      
                      <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="job12">
                        <ItemTemplate>
                            <asp:Label ID="lblgvjob12" runat="server" BackColor="Transparent"
                                BorderStyle="None"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job12").ToString())%>'
                                Width="100px"></asp:Label>
                             <asp:TextBox ID="txtgvjob12" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job12")) %>'></asp:TextBox>



                            <asp:TextBox ID="txtgvjobd12" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job12")) %>'></asp:TextBox>
                            <cc1:CalendarExtender ID="txtgvjobd12_CalendarExtender" runat="server"
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvjobd12"></cc1:CalendarExtender>


                            <asp:DropDownList ID="ddlassignuser12" runat="server" CssClass=" inputTxt form-control"
                                TabIndex="12">
                            </asp:DropDownList>


                        </ItemTemplate>
                         <FooterTemplate>

                            <asp:LinkButton ID="lbtnDeleteJob12" runat="server" OnClick="lbtnDeleteJob_Click" CommandArgument="12" ToolTip="Remove Job"><i  style="color:red"  class=" fa fa-trash"></i></asp:LinkButton>

                        </FooterTemplate>
                      
                      <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="job13">
                        <ItemTemplate>
                            <asp:Label ID="lblgvjob13" runat="server" BackColor="Transparent"
                                BorderStyle="None"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job13").ToString())%>'
                                Width="100px"></asp:Label>

                             <asp:TextBox ID="txtgvjob13" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job13")) %>'></asp:TextBox>



                            <asp:TextBox ID="txtgvjobd13" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job13")) %>'></asp:TextBox>
                            <cc1:CalendarExtender ID="txtgvjobd13_CalendarExtender" runat="server"
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvjobd13"></cc1:CalendarExtender>


                            <asp:DropDownList ID="ddlassignuser13" runat="server" CssClass=" inputTxt form-control"
                                TabIndex="12">
                            </asp:DropDownList>

                        </ItemTemplate>
                         <FooterTemplate>

                            <asp:LinkButton ID="lbtnDeleteJob13" runat="server" OnClick="lbtnDeleteJob_Click" CommandArgument="13" ToolTip="Remove Job"><i  style="color:red"  class=" fa fa-trash"></i></asp:LinkButton>

                        </FooterTemplate>
                      
                      <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="job14">
                        <ItemTemplate>
                            <asp:Label ID="lblgvjob14" runat="server" BackColor="Transparent"
                                BorderStyle="None"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job14").ToString())%>'
                                Width="100px"></asp:Label>


                             <asp:TextBox ID="txtgvjob14" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job14")) %>'></asp:TextBox>



                            <asp:TextBox ID="txtgvjobd14" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job14")) %>'></asp:TextBox>
                            <cc1:CalendarExtender ID="txtgvjobd14_CalendarExtender" runat="server"
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvjobd14"></cc1:CalendarExtender>


                            <asp:DropDownList ID="ddlassignuser14" runat="server" CssClass=" inputTxt form-control"
                                TabIndex="12">
                            </asp:DropDownList>

                        </ItemTemplate>
                         <FooterTemplate>

                            <asp:LinkButton ID="lbtnDeleteJob14" runat="server" OnClick="lbtnDeleteJob_Click" CommandArgument="14" ToolTip="Remove Job"><i  style="color:red"  class=" fa fa-trash"></i></asp:LinkButton>

                        </FooterTemplate>
                      
                      <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>


                    
                    <asp:TemplateField HeaderText="Code">

                       

                        <ItemTemplate>
                            <asp:Label ID="lblgvcode" runat="server" BackColor="Transparent"
                                BorderStyle="None"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod"))%>'
                                Width="60px" Style="text-align: right"></asp:Label>
                        </ItemTemplate>


                        <ItemStyle HorizontalAlign="right" />
                        <FooterStyle HorizontalAlign="Right" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>






                </Columns>
                <PagerStyle CssClass="gvPagination" />
                <HeaderStyle CssClass="grvHeader" />
                <FooterStyle CssClass="grvFooter" />
                <RowStyle CssClass="grvRows" />
            </asp:GridView>


        </div>
    </div>

    <%--Modal  --%>

    <div id="AddComments" class="modal animated slideInLeft " role="dialog" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog">
            <div class="modal-content  ">
                <div class="modal-header">


                    <h4 class="modal-title">
                        <span class="fa fa-table"></span>Add New Comments  </h4>

                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true" class="white-text">&times;</span>
                    </button>
                </div>
                <div class="modal-body form-horizontal">
                    <div class="row-fluid">
                        <asp:Label ID="lblactcode" runat="server" Visible="false"></asp:Label>

                        <div class="form-group" runat="server">
                            <label class="col-md-4">Comments</label>


                            <div class="col-md-10">
                                <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group" runat="server">
                            <label class="col-md-4">Previous Comments</label>
                            <asp:GridView ID="gvComm" runat="server" AutoGenerateColumns="False" CssClass="table-condensed table-hover table-bordered grvContentarea"
                                ShowFooter="True" Width="150px" Style="margin-right: 0px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Date">

                                        <ItemTemplate>
                                            <asp:Label ID="txtAcStDate" runat="server" BackColor="Transparent"
                                                BorderStyle="None"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cdate")).ToString("dd-MMM-yyyy")%>'
                                                Width="65px"></asp:Label>

                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>




                                    <asp:TemplateField HeaderText="Comments">

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvcomments" runat="server" BackColor="Transparent"
                                                BorderStyle="None"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comments").ToString())%>'
                                                Width="280px" Font-Size="11px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="User">

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvusername" runat="server" BackColor="Transparent"
                                                BorderStyle="None"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "username").ToString())%>'
                                                Width="80px" Font-Size="11px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>





                                </Columns>
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                                <FooterStyle CssClass="grvFooter" />
                                <RowStyle CssClass="grvRows" />
                            </asp:GridView>

                        </div>
                    </div>


                </div>
                <div class="modal-footer ">
                    <asp:LinkButton ID="lbtnAddCode" runat="server" CssClass="btn btn-sm btn-success" OnClientClick="CloseModal();" OnClick="lbtnAddCode_Click"><span class="glyphicon glyphicon-save"></span> Update </asp:LinkButton>



                </div>
            </div>
        </div>
    </div>

    <div id="AddUser" class="modal animated slideInLeft " role="dialog" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog">
            <div class="modal-content  ">
                <div class="modal-header">


                    <h4 class="modal-title">
                        <span class="fa fa-table"></span>Add New User  </h4>

                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true" class="white-text">&times;</span>
                    </button>
                </div>
                <div class="modal-body form-horizontal">
                    <div class="row-fluid">
                        <asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>

                        <div class="form-group" runat="server">
                            <%--<label class="col-md-4">Comments</label>--%>


                            <div class="col-md-10">
                                <asp:DropDownList ID="ddlUserList" runat="server" CssClass=" form-control inputTxt">
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>


                </div>
                <div class="modal-footer ">
                    <asp:LinkButton ID="lbtnUpdateUsr" runat="server" CssClass="btn btn-sm btn-success" OnClientClick="CloseModal();" OnClick="lbtnUpdateUsr_Click"><span class="glyphicon glyphicon-save"></span> Update </asp:LinkButton>



                </div>
            </div>
        </div>
    </div>



    <div class="modal fade" id="AddDoc" tabindex="-1" role="dialog" aria-labelledby="followingModalLabel" aria-hidden="true">
        <!-- .modal-dialog -->
        <div class="modal-dialog modal-dialog-scrollable" role="document">
            <!-- .modal-content -->
            <div class="modal-content">
                <!-- .modal-header -->
                <div class="modal-header">
                    <h6 id="followingModalLabel" class="modal-title"><span class="fa fa-user-tag"></span>Upload more documents </h6>
                </div>
                <!-- /.modal-header -->
                <!-- .modal-body -->
                <div class="modal-body px-0" style="min-height: 140px;">

                    <div class="card-body">
                        <div id="dropzone" class="fileinput-dropzone">
                            <span>Drop files or click to upload.</span>
                            <!-- The file input field used as target for the file upload widget -->

                            <cc1:AsyncFileUpload runat="server"
                                ID="AsyncFileUpload1" UploaderStyle="Modern"
                                CompleteBackColor="White"
                                UploadingBackColor="#CCFFFF" ThrobberID="imgLoader"
                                OnUploadedComplete="FileUploadComplete" />
                        </div>
                        <div id="progress" class="progress progress-xs rounded-0 fade">
                            <div class="progress-bar progress-bar-striped progress-bar-animated bg-success" role="progressbar" aria-valuemin="0" aria-valuemax="100"></div>
                        </div>

                        <asp:FileUpload ID="fileuploaddropzone" Visible="false" runat="server" />

                    </div>

                    <div class="modal-footer">
                        <asp:Label ID="lblImcode" runat="server" Visible="false"></asp:Label>

                        <asp:ListView ID="ListViewEmpAll" runat="server" ItemPlaceholderID="itemplaceholder" OnItemDataBound="ListViewEmpAll_ItemDataBound">
                            <LayoutTemplate>
                                <asp:PlaceHolder ID="itemplaceholder" runat="server" />
                            </LayoutTemplate>
                            <ItemTemplate>
                                <div class="col-xs-12 col-sm-4 col-md-2 listDiv" style="padding: 0 5px;">
                                    <div id="EmpAll" runat="server">

                                        <asp:Label ID="ImgLink" Visible="false" runat="server" Text='<%# Eval("docurl") %>'></asp:Label>
                                        <asp:Label ID="lblcdate" Visible="false" runat="server" Text='<%# Eval("cdate") %>'></asp:Label>


                                        <a href="<%# ResolveUrl("~/"+Eval("docurl").ToString()) %>" class="uploadedimg" target="_blank">
                                            <asp:Image ID="GetImg" runat="server" Style="min-height: 70px;" CssClass="image img img-responsive img-thumbnail" />
                                            <div class="middle">
                                                <span><%# Eval("username") %></span>
                                            </div>
                                            <div class="checkboxcls">
                                                <asp:CheckBox ID="ChDel" runat="server" />
                                            </div>
                                        </a>



                                    </div>

                                </div>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>

                </div>
                <!-- /.modal-body -->
                <!-- .modal-footer -->
                <div class="modal-footer">








                    <asp:LinkButton ID="btnDelall" runat="server" OnClick="btnDelall_OnClick" Visible="true" CssClass=" btn btn-xs btn-danger">Delete</asp:LinkButton>


                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
                <!-- /.modal-footer -->
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>



    <%--            
        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>





