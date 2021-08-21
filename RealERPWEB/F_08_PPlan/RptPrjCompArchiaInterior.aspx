<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptPrjCompArchiaInterior.aspx.cs" Inherits="RealERPWEB.F_08_PPlan.RptPrjCompArchiaInterior" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });


        function pageLoaded() {
            try {

                $(".chosen-select").chosen({
                    search_contains: true,
                    no_results_text: "Sorry, no match!",
                    allow_single_deselect: true
                });
                $('.chosen-continer').css({ 'width': '600px', "height": "20px" });

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



        .container-data {
            box-sizing: border-box;
        }
    </style>


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
                <div class="card-body" style="min-height: 600px;">
                    <div class="form-group">
                        <div class="col-md-12 ">
                            <div class="row">
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <label class="control-label" for="project" id="lblproject" runat="server">Project Name</label>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlPrjName" runat="server" CssClass=" form-control chzn-select">
                                        </asp:DropDownList>
                                    </div>
                                </div>                               

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass=" btn btn-primary  " OnClick="lbtnOk_Click" Style="margin-left: 20px;">Ok</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>



                    <asp:GridView ID="gvPrjInfo" runat="server" AutoGenerateColumns="False" CssClass="table-condensed table-hover table-bordered grvContentarea"
                        ShowFooter="True" OnRowDataBound="gvPrjInfo_RowDataBound">
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

                            <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvItemDesc" runat="server" Font-Size="12px" Font-Bold="true" ForeColor="blue"
                                            Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc"))   %>'
                                            Width="110px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                            <asp:TemplateField HeaderText="Estimate Amount">

                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvestamt" runat="server" BackColor="Transparent"
                                        BorderStyle="None"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proesam")).ToString("#,##0;-#,##0; ")%>'
                                        Width="50px" Style="text-align: right"></asp:TextBox>
                                </ItemTemplate>


                                <ItemStyle HorizontalAlign="right" />
                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Advanced Amount">

                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvadvamt" runat="server" BackColor="Transparent"
                                        BorderStyle="None"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proadam")).ToString("#,##0;-#,##0; ")%>'
                                        Width="50px" Style="text-align: right"></asp:TextBox>
                                </ItemTemplate>


                                <ItemStyle HorizontalAlign="right" />
                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Due Amount">

                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvdueamt" runat="server" BackColor="Transparent"
                                        BorderStyle="None"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam")).ToString("#,##0;-#,##0; ")%>'
                                        Width="50px" Style="text-align: right"></asp:TextBox>
                                </ItemTemplate>


                                <ItemStyle HorizontalAlign="right" />
                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="job1">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvjob1" runat="server" BackColor="Transparent" BorderStyle="None"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job1").ToString())%>' Width="100px">
                                    </asp:Label>

                                    <asp:Label ID="txtgvjob1" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job1")) %>'> </asp:Label>

                                    <asp:Label ID="txtgvjobd1" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job1")) %>'></asp:Label>

                                    <asp:Label ID="txtgvassignuser1" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job1")) %>'></asp:Label>

                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="job2">
                                <ItemTemplate>

                                    <asp:Label ID="lblgvjob2" runat="server" BackColor="Transparent" BorderStyle="None"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job2").ToString())%>' Width="100px">
                                    </asp:Label>

                                    <asp:Label ID="txtgvjob2" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job2")) %>'> </asp:Label>

                                    <asp:Label ID="txtgvjobd2" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job2")) %>'></asp:Label>

                                    <asp:Label ID="txtgvassignuser2" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job2")) %>'></asp:Label>


                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="job3">
                                <ItemTemplate>

                                    <asp:Label ID="lblgvjob3" runat="server" BackColor="Transparent" BorderStyle="None"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job3").ToString())%>' Width="100px">
                                    </asp:Label>

                                    <asp:Label ID="txtgvjob3" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job3")) %>'> </asp:Label>

                                    <asp:Label ID="txtgvjobd3" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job3")) %>'></asp:Label>

                                    <asp:Label ID="txtgvassignuser3" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job3")) %>'></asp:Label>


                                </ItemTemplate>


                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="job4">
                                <ItemTemplate>

                                    <asp:Label ID="lblgvjob4" runat="server" BackColor="Transparent" BorderStyle="None"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job4").ToString())%>' Width="100px">
                                    </asp:Label>

                                    <asp:Label ID="txtgvjob4" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job4")) %>'> </asp:Label>

                                    <asp:Label ID="txtgvjobd4" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job4")) %>'></asp:Label>

                                    <asp:Label ID="txtgvassignuser4" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job4")) %>'></asp:Label>


                                </ItemTemplate>

                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="job5">
                                <ItemTemplate>

                                    <asp:Label ID="lblgvjob5" runat="server" BackColor="Transparent" BorderStyle="None"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job5").ToString())%>' Width="100px">
                                    </asp:Label>

                                    <asp:Label ID="txtgvjob5" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job5")) %>'> </asp:Label>

                                    <asp:Label ID="txtgvjobd5" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job5")) %>'></asp:Label>

                                    <asp:Label ID="txtgvassignuser5" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job5")) %>'></asp:Label>


                                </ItemTemplate>

                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="job6">

                                <ItemTemplate>

                                    <asp:Label ID="lblgvjob6" runat="server" BackColor="Transparent" BorderStyle="None"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job6").ToString())%>' Width="100px">
                                    </asp:Label>

                                    <asp:Label ID="txtgvjob6" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job6")) %>'> </asp:Label>

                                    <asp:Label ID="txtgvjobd6" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job6")) %>'></asp:Label>

                                    <asp:Label ID="txtgvassignuser6" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job6")) %>'></asp:Label>


                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="job7">
                                <ItemTemplate>

                                    <asp:Label ID="lblgvjob7" runat="server" BackColor="Transparent" BorderStyle="None"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job7").ToString())%>' Width="100px">
                                    </asp:Label>

                                    <asp:Label ID="txtgvjob7" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job7")) %>'> </asp:Label>

                                    <asp:Label ID="txtgvjobd7" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job7")) %>'></asp:Label>

                                    <asp:Label ID="txtgvassignuser7" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job7")) %>'></asp:Label>


                                </ItemTemplate>

                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="job8">
                                <ItemTemplate>

                                    <asp:Label ID="lblgvjob8" runat="server" BackColor="Transparent" BorderStyle="None"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job8").ToString())%>' Width="100px">
                                    </asp:Label>

                                    <asp:Label ID="txtgvjob8" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job8")) %>'> </asp:Label>

                                    <asp:Label ID="txtgvjobd8" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job8")) %>'></asp:Label>

                                    <asp:Label ID="txtgvassignuser8" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job8")) %>'></asp:Label>


                                </ItemTemplate>

                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="job9">

                                <ItemTemplate>
                                    <asp:Label ID="lblgvjob9" runat="server" BackColor="Transparent" BorderStyle="None"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job9").ToString())%>' Width="100px">
                                    </asp:Label>

                                    <asp:Label ID="txtgvjob9" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job9")) %>'> </asp:Label>

                                    <asp:Label ID="txtgvjobd9" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job9")) %>'></asp:Label>

                                    <asp:Label ID="txtgvassignuser9" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job9")) %>'></asp:Label>


                                </ItemTemplate>

                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="job10">
                                <ItemTemplate>

                                    <asp:Label ID="lblgvjob10" runat="server" BackColor="Transparent" BorderStyle="None"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job10").ToString())%>' Width="100px">
                                    </asp:Label>

                                    <asp:Label ID="txtgvjob10" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job10")) %>'> </asp:Label>

                                    <asp:Label ID="txtgvjobd10" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job10")) %>'></asp:Label>

                                    <asp:Label ID="txtgvassignuser10" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job10")) %>'></asp:Label>


                                </ItemTemplate>

                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="job11">
                                <ItemTemplate>

                                    <asp:Label ID="lblgvjob11" runat="server" BackColor="Transparent" BorderStyle="None"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job11").ToString())%>' Width="100px">
                                    </asp:Label>

                                    <asp:Label ID="txtgvjob11" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job11")) %>'> </asp:Label>

                                    <asp:Label ID="txtgvjobd11" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job11")) %>'></asp:Label>

                                    <asp:Label ID="txtgvassignuser11" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job11")) %>'></asp:Label>


                                </ItemTemplate>

                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="job12">

                                <ItemTemplate>

                                    <asp:Label ID="lblgvjob12" runat="server" BackColor="Transparent" BorderStyle="None"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job12").ToString())%>' Width="100px">
                                    </asp:Label>

                                    <asp:Label ID="txtgvjob12" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job12")) %>'> </asp:Label>

                                    <asp:Label ID="txtgvjobd12" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job12")) %>'></asp:Label>

                                    <asp:Label ID="txtgvassignuser12" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job12")) %>'></asp:Label>


                                </ItemTemplate>

                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="job13">

                                <ItemTemplate>

                                    <asp:Label ID="lblgvjob13" runat="server" BackColor="Transparent" BorderStyle="None"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job13").ToString())%>' Width="100px">
                                    </asp:Label>

                                    <asp:Label ID="txtgvjob13" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job13")) %>'> </asp:Label>

                                    <asp:Label ID="txtgvjobd13" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job13")) %>'></asp:Label>

                                    <asp:Label ID="txtgvassignuser13" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job13")) %>'></asp:Label>


                                </ItemTemplate>

                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="job14">
                                <ItemTemplate>

                                    <asp:Label ID="lblgvjob14" runat="server" BackColor="Transparent" BorderStyle="None"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job14").ToString())%>' Width="100px">
                                    </asp:Label>

                                    <asp:Label ID="txtgvjob14" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job14")) %>'> </asp:Label>

                                    <asp:Label ID="txtgvjobd14" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job14")) %>'></asp:Label>

                                    <asp:Label ID="txtgvassignuser14" runat="server" BorderWidth="0" Style="width: 100px; float: left;" BackColor="Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job14")) %>'></asp:Label>


                                </ItemTemplate>
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



        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>






