﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="SaleSurvey.aspx.cs" Inherits="RealERPWEB.F_22_Sal.SaleSurvey" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

   

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }

        .lnkbtmfromtop {
            margin-top: 28px;
            margin-left: 5px;
        }

        .srchbtmfromtop {
            margin-top: 5px !important;
        }

        .grvContentarea {
        }
    </style>


    <script language="javascript" type="text/javascript">
        $(document).ready(function () {

            $(".select2").select2();

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            try {



                $("input, select").bind("keydown", function (event) {
                    var k1 = new KeyPress();
                    k1.textBoxHandler(event);
                });

                $('.select2').each(function () {
                    var select = $(this);
                    select.select2({
                        placeholder: 'Select an option',
                        width: '100%',
                        allowClear: !select.prop('required'),
                        language: {
                            noResults: function () {
                                return "{{ __('No results found') }}";
                            }
                        }
                    });
                });


                $('.chzn-select').chosen({ search_contains: true });
                var gridview = $('#<%=this.gvMSRInfo2.ClientID %>');
                $.keynavigation(gridview);
            }
            catch (e)
            {
                alert(e.message);

            }
        }
    </script>






    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>




            <div class="card card-fluid mb-2">
                <div class="card-body">

                    <div class="row">

                        <div class="col-md-1">
                            <div class="form-group">
                                <label>Survey No</label>
                                <asp:Label ID="lblCurMSRNo1" runat="server" CssClass="form-control form-control-sm"></asp:Label>


                            </div>

                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <label>Date</label>
                                <asp:TextBox ID="txtCurMSRDate" runat="server" CssClass=" form-control form-control-sm" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtCurMSRDate_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd.MM.yyyy" TargetControlID="txtCurMSRDate"></cc1:CalendarExtender>


                            </div>

                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label id="lblprevious" runat="server">
                                    Previous
                                    <asp:LinkButton ID="lnkbtnFindPreMR" runat="server" OnClick="lnkbtnFindPreMR_Click"> <i class="fa fa-search" aria-hidden="true"></i>
                                    </asp:LinkButton></label>

                                <asp:DropDownList ID="ddlPrevMSRList" runat="server" CssClass="form-control form-control-sm  chzn-select"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">

                                <asp:LinkButton ID="lbtnMSROk" runat="server" CssClass=" btn btn-primary btn-sm lnkbtmfromtop" OnClick="lbtnMSROk_Click">Ok</asp:LinkButton>

                            </div>

                        </div>

                    </div>

                </div>


            </div>

            <div class="card card-fluid  mb-2">
                <div class="row">


                    <div class="col-md-3">
                        <div class="from-group">
                            <label id="lblprojectname" runat="server" visible="false">Project Name</label>
                            <asp:ListBox ID="chkProjectName" runat="server" ClientIDMode="Static" CssClass="form-control form-control-sm  select2" Style="min-width: 200px !important;" SelectionMode="Multiple" Visible="false"></asp:ListBox>

                        </div>
                    </div>

                    <div class="col-md-1">
                        <div class="form-group">

                            <asp:LinkButton ID="lbtnSelect" runat="server" CssClass=" btn btn-primary btn-sm lnkbtmfromtop" OnClick="lbtnSelect_Click" Visible="false">Select</asp:LinkButton>

                        </div>

                    </div>






                </div>

            </div>




            <asp:GridView ID="gvMSRInfo2" runat="server" CssClass=" table-striped table-bordered grvContentarea"
                AutoGenerateColumns="False" ShowFooter="True"
                Width="1225px" Font-Size="12px">
                <PagerSettings Visible="False" />
                <Columns>
                    <asp:TemplateField HeaderText="Sl">
                        <ItemTemplate>
                            <asp:Label ID="lblgvMSRSlNo" runat="server" Height="16px"
                                Style="text-align: right"
                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>





                    <asp:TemplateField HeaderText=" Developer">

                        <FooterTemplate>
                            <asp:LinkButton ID="lbtnTotal" runat="server" OnClick="lbtnTotal_Click" CssClass="btn btn-warning  btn-sm ">Total</asp:LinkButton>
                        </FooterTemplate>

                        <ItemTemplate>
                            <asp:Label ID="lblgvdeveloper" runat="server"
                                Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname"))   %>'
                                Width="120px">
                            </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>


                      <asp:TemplateField HeaderText="Project">

                        <ItemTemplate>
                            <asp:Label ID="lblgvproject" runat="server"
                                Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))   %>'
                                Width="150px">
                            </asp:Label>
                        </ItemTemplate>
                       

                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Building Type">

                        <ItemTemplate>
                            <asp:Label ID="lblgvbuildtype" runat="server"
                                Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "buildtype"))   %>'
                                Width="150px">
                            </asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:LinkButton ID="lbtnMSRUpdate" runat="server" CssClass="btn btn-success btn-sm" OnClientClick="return Confirmation();" OnClick="lbtnMSRUpdate_Click">Final Update</asp:LinkButton>
                        </FooterTemplate>

                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Location">

                        <ItemTemplate>
                            <asp:Label ID="lblgvLocation" runat="server"
                                Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "location"))   %>'
                                Width="150px">
                            </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Apt. Size(sft)">

                        <ItemTemplate>
                            <asp:Label ID="lblgvaptsize" runat="server"
                                Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "aptsize"))   %>'
                                Width="100px">
                            </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Land Area(katha)">

                        <ItemTemplate>
                            <asp:Label ID="lblgvlandarea" runat="server"
                                Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "landarea")).ToString("#,##0.00;(#,##0.00); ")   %>'
                                Width="80px">
                            </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="  Storied">

                        <ItemTemplate>
                            <asp:Label ID="lblgvstoried" runat="server"
                                Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "storied"))   %>'
                                Width="100px">
                            </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Total Apt. Units.(Nos)">

                        <ItemTemplate>
                            <asp:Label ID="lblgvaptunit" runat="server"
                                Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aptunit")).ToString("#,##0.00;(#,##0.00); ")   %>'
                                Width="70px">
                            </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="Asking Price(TK/sft)">

                        <ItemTemplate>
                            <asp:Label ID="lblgvaskingprice" runat="server"
                                Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "askingprice")).ToString("#,##0.00;(#,##0.00); ")   %>'
                                Width="70px">
                            </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Selling Price(TK/sft)">

                        <ItemTemplate>
                            <asp:Label ID="lblgvselprice" runat="server"
                                Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "selprice")).ToString("#,##0.00;(#,##0.00); ")   %>'
                                Width="70px">
                            </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Utility Price(TK in Lac)">

                        <ItemTemplate>
                            <asp:Label ID="lblgvutlityprice" runat="server"
                                Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "utlityprice")).ToString("#,##0.00;(#,##0.00); ")   %>'
                                Width="70px">
                            </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Parking Price(TK in Lac)">

                        <ItemTemplate>
                            <asp:Label ID="lblgvprkingpirce" runat="server"
                                Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prkingpirce")).ToString("#,##0.00;(#,##0.00); ")   %>'
                                Width="70px">
                            </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>




                    <asp:TemplateField HeaderText="Handover Date">

                        <ItemTemplate>
                            <asp:Label ID="lblgvhoverdate" runat="server"
                                Text='<%#  Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "hoverdate")).ToString("dd-MMM-yyyy")   %>'
                                Width="75px">
                            </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Catagory">

                        <ItemTemplate>
                            <asp:Label ID="lblgvcatagory" runat="server"
                                Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "procatagory"))   %>'
                                Width="100px">
                            </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Comments">

                        <ItemTemplate>
                            <asp:TextBox ID="txtgvcomments" runat="server" BorderStyle="None" BackColor="Transparent"
                                Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "comments"))   %>'
                                Width="100px">
                            </asp:TextBox>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>






                </Columns>
                <FooterStyle CssClass="grvFooterNew" />
                <EditRowStyle />
                <AlternatingRowStyle />
                <PagerStyle CssClass="" />
                <HeaderStyle CssClass="grvHeaderNew" />
            </asp:GridView>







        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

