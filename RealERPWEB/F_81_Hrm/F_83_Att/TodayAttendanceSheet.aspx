<%@ Page Title="" Language="C#"  MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="TodayAttendanceSheet.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_83_Att.TodayAttendanceSheet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

      <style type="text/css">
        .modalcss {
            margin: 0;
            padding: 0;
            width: 100%;
            height: 100%;
            position: fixed;
            overflow: scroll;
        }

        .multiselect {
            width: 233px !important;
            text-wrap: initial !important;
            height: 27px !important;
        }

        .multiselect-text {
            width: 200px !important;
        }

        .multiselect-container {
            height: 250px !important;
            width: 250px !important;
            overflow-y: scroll !important;
        }

        span.multiselect-selected-text {
            width: 200px !important;
        }

        .rbtnAtten tbody tr td {
            margin: 0 5px;
        }

            .rbtnAtten tbody tr td input[type=checkbox], .rbtnAtten tbody tr td input[type=radio] {
                box-sizing: border-box;
                padding: 0;
                margin: 0 0 0 12px;
            }

            .rbtnAtten tbody tr td label {
                margin: 0 0 0 5px;
            }
	.form-group{
margin-bottom:05px;
}
    </style>


    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>

      <script type="text/javascript">

          $(document).ready(function () {
              $(".select2").select2();
              Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

          });
          function pageLoaded() {
              $('.chzn-select').chosen({ search_contains: true });
              $(".chosen-select").chosen({
                  search_contains: true,
                  no_results_text: "Sorry, no match!",
                  allow_single_deselect: true
              });


          <%-- var gvMonthlyattSummary = $('#<%=this.gvMonthlyattSummary.ClientID %>');
            gvMonthlyattSummary.Scrollable();--%>


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
             <div class="card card-fluid mt-3" style="min-height: 550px;">
                <div class="card-header mb-0">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="ddlLvType">
                                    Company  
                                </label>
                                <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"  AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="ddlLvType">
                                    Department  
                                </label>
                                <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3" id="PnlSection" runat="server">
                            <div class="form-group">
                                <label for="ddlLvType">
                                    Section  
                                </label>
                                <asp:ListBox ID="DropCheck1" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="DropCheck1_SelectedIndexChanged" SelectionMode="Multiple" ></asp:ListBox>
                            </div>
                        </div>

                     
                   
                        <div class="col-md-2" id="Div1" runat="server">
                            <div class="form-group">
                                <asp:Label ID="lblfrmdate" runat="server" style="display:block; margin-bottom:8px;">Date</asp:Label>
 
                                <asp:TextBox ID="txtfromdate" runat="server" CssClass=" form-control  form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy"  TargetControlID="txtfromdate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-1">
                            <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary btn-sm mt-4" OnClick="lnkbtnShow_Click" >Show</asp:LinkButton>
                        </div>
                    </div>


                </div>

<div class="card-body">
                 <div class="table-responsive">
                                <asp:GridView ID="gvdailyatt" runat="server" PageSize="15"
                                    AutoGenerateColumns="False" ShowFooter="True" 
                                    CssClass="table-striped table-hover table-bordered grvContentarea">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSl" runat="server" CssClass="mr-2 text-center"   Font-Bold="True" Height="14px"  Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" CssClass="mr-2 text-center"  VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                      
                                        <asp:TemplateField HeaderText="Card #">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcardno" runat="server" CssClass="col-" Font-Size="12px" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />

                                            <HeaderStyle HorizontalAlign="Center" CssClass="col-" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Section">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsection" runat="server" CssClass="col-" Font-Size="12px" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                      Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />

                                            <HeaderStyle HorizontalAlign="Center" CssClass="col-" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvempname" runat="server"    Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                    Width="250px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />

                                            <HeaderStyle HorizontalAlign="Center"  CssClass="mr-4 text-center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdsignation" runat="server"  CssClass="mr-4"  Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />

                                            <HeaderStyle HorizontalAlign="Center"  CssClass="mr-4 text-center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Office In">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvoffin"  runat="server" CssClass="mr-4 text-center"  Font-Size="11px" Text='<%#  Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "offintime")).ToString("hh:mm tt") %>'
                                                    Width="45px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" CssClass="mr-4 text-center"  VerticalAlign="Middle" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText=" Office Out">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvoffout" runat="server" CssClass="mr-4 text-center" Text='<%#  Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "offouttime")).ToString("hh:mm tt") %>'
                                                    Width="45px" Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" CssClass="mr-4 text-center"  VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Punce In">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvactin" CssClass="mr-4 text-center" runat="server" Font-Size="11px" Text='<%#  Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "intime")).ToString("hh:mm tt") %>'
                                                    Width="45px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" CssClass="mr-4 text-center" VerticalAlign="Middle" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Punce Out">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvactout" runat="server" CssClass="mr-4 text-center" Text='<%#  Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "outtime")).ToString("hh:mm tt") %>'
                                                    Width="45px" Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" CssClass="mr-4 text-center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Late">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvlate" runat="server" CssClass="mr-4 text-center" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "late")) %>'
                                                    Width="33px" Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" CssClass="mr-4 text-center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Early <br/> Leave">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvearlylv" runat="server" CssClass="mr-4 text-center"  Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "eleave")) %>'
                                                    Width="33px" Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" CssClass="mr-4 text-center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Leave <br/>/ Absent">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvabs" runat="server" class="mr-4"  CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "status"))=="A"?"bg-red d-block":
                                                     ""%>'  Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "status")) %>'
                                                    Width="33px" Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" CssClass="mr-4 text-center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                       


                                    </Columns>
                                    <FooterStyle BackColor="#F5F5F5" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                                </asp:GridView>
                            </div>

 </div>


                 </div>


            </ContentTemplate>
         </asp:UpdatePanel>

</asp:Content>